using eQACoLTD.Application.Common;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.DBContext;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Payment.Handlers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Product.Payment.Queries;

namespace eQACoLTD.Application.Product.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly AppIdentityDbContext _context;
        public PaymentService(AppIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<string>> OrderReceiveAsync(string accountId,string orderId, OrderPaymenForCreationDto creationDto)
        {
            var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if (checkEmployee == null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
            var checkOrder = await _context.Orders.FindAsync(orderId);
            if (checkOrder == null) return new ApiResult<string>(HttpStatusCode.NotFound, ($"Không tìm thấy đơn hàng có mã: {orderId}"));
            if (checkOrder.TransactionStatusId != GlobalProperties.FinishedTransactionId 
            && checkOrder.TransactionStatusId!=GlobalProperties.WaitingTransactionId
            && checkOrder.TransactionStatusId!=GlobalProperties.CancelTransactionId)
            {
                if (checkEmployee.BranchId == checkOrder.BranchId)
                {
                    var receiptVoucher = ObjectMapper.Mapper.Map<ReceiptVoucher>(creationDto);
                    var sequenceNumber = await _context.ReceiptVouchers.CountAsync();
                    var receiptVoucherId = IdentifyGenerator.GenerateReceiptVoucherId(sequenceNumber + 1);
                    receiptVoucher.Id = receiptVoucherId;
                    receiptVoucher.OrderId = checkOrder.Id;
                    receiptVoucher.DateCreated = DateTime.Now;
                    receiptVoucher.CustomerId = checkOrder.CustomerId;
                    receiptVoucher.IsDelete = false;
                    receiptVoucher.BranchId = checkOrder.BranchId;
                    receiptVoucher.EmployeeId = checkEmployee.Id;
                    await _context.ReceiptVouchers.AddAsync(receiptVoucher);
                    await _context.SaveChangesAsync();
                    var totalPaid = await (from rv in _context.ReceiptVouchers
                                           where rv.OrderId == checkOrder.Id
                                           select rv.Received).SumAsync();
                    var checkExportStock = await _context.GoodsDeliveryNotes.Where(x => x.OrderId == checkOrder.Id)
                        .SingleOrDefaultAsync();
                    if (totalPaid == checkOrder.TotalAmount)
                    {
                        if (checkExportStock != null)
                        {
                            checkOrder.TransactionStatusId = GlobalProperties.FinishedTransactionId;   
                        }
                        checkOrder.PaymentStatusId = GlobalProperties.PaidPaymentId;
                    }
                    else if (totalPaid < checkOrder.TotalAmount)
                    {
                        checkOrder.PaymentStatusId = GlobalProperties.PartialPaymentId;
                    }
                    else return new ApiResult<string>(HttpStatusCode.BadRequest,$"Giá trị thanh toán lớn hơn giá trị đơn hàng");
                    await _context.SaveChangesAsync();
                    return new ApiResult<string>(HttpStatusCode.OK) { ResultObj = receiptVoucherId,Message = "Đã tạo phiếu thu cho đơn hàng"};
                }
                return new ApiResult<string>(HttpStatusCode.Forbidden, $"Tài khoản đăng nhập hiện tại không có quyền chỉnh sửa phiếu thu tại chi nhánh này");

            }
            return new ApiResult<string>(HttpStatusCode.BadRequest, "Chỉ được tạo phiếu thu cho đơn hàng đang trọng trạng thái giao dịch.");
        }

        public async Task<ApiResult<string>> PurchaseOrderPaymentAsync(string accountId, string purchaseOrderId, 
            PurchaseOrderPaymentForCreationDto creationDto)
        {
            var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if (checkEmployee == null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
            var checkPurchaseOrder = await _context.PurchaseOrders.FindAsync(purchaseOrderId);
            if (checkPurchaseOrder == null) return new ApiResult<string>(HttpStatusCode.NotFound);
            if (checkEmployee.BranchId == checkPurchaseOrder.BrandId)
            {
                var sequenceNumber = await _context.PaymentVouchers.CountAsync();
                var paymentVoucherId = IdentifyGenerator.GeneratePaymentVoucherId(sequenceNumber + 1);
                var paymentVoucher = ObjectMapper.Mapper.Map<PaymentVoucher>(creationDto);
                paymentVoucher.Id = paymentVoucherId;
                paymentVoucher.PurchaseOrderId = purchaseOrderId;
                paymentVoucher.DateCreated = DateTime.Now;
                paymentVoucher.SupplierId = checkPurchaseOrder.SupplierId;
                paymentVoucher.EmployeeId = checkEmployee.Id;
                paymentVoucher.BranchId = checkEmployee.BranchId;
                await _context.PaymentVouchers.AddAsync(paymentVoucher);
                var totalPayment = await (from pv in _context.PaymentVouchers
                                          where pv.PurchaseOrderId == purchaseOrderId
                                          select pv.Paid).SumAsync();
                if (totalPayment - checkPurchaseOrder.TotalAmount >=0)
                {
                    checkPurchaseOrder.TransactionStatusId = GlobalProperties.FinishedTransactionId;
                    checkPurchaseOrder.PaymentStatusId = GlobalProperties.PaidPaymentId;
                }
                else if(checkPurchaseOrder.TotalAmount-totalPayment>0)
                {
                    checkPurchaseOrder.TransactionStatusId = GlobalProperties.TradingTransactionId;
                    checkPurchaseOrder.PaymentStatusId = GlobalProperties.PartialPaymentId;
                }
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK) { ResultObj = paymentVoucherId,Message = "Tạo phiếu chi cho đơn hàng thành công"};

            }
            return new ApiResult<string>(HttpStatusCode.Forbidden, $"Tài khoản hiện tại không có quyền chỉnh sửa phiếu chi tại chi nhánh này");
        }

        public async Task<ApiResult<bool>> IsPaidOrder(string orderId)
        {
            var checkOrder = await _context.Orders.FindAsync(orderId);
            if(checkOrder==null) return new ApiResult<bool>(HttpStatusCode.NotFound,$"Không tìm thấy đơn hàng có mã: {orderId}");
            var totalPaid = await (from rv in _context.ReceiptVouchers
                where rv.OrderId == checkOrder.Id
                select rv.Received).SumAsync();
            if (checkOrder.TotalAmount == totalPaid) return new ApiResult<bool>(HttpStatusCode.OK,true);
            return new ApiResult<bool>(HttpStatusCode.OK,false);
        }

        public async Task<ApiResult<IEnumerable<OrderPaymentsDto>>> GetOrderPaymentHistory(string orderId)
        {
            var checkOrder = await _context.Orders.FindAsync(orderId);
            if(checkOrder==null) return new ApiResult<IEnumerable<OrderPaymentsDto>>(HttpStatusCode.NotFound,$"Không tìm thấy đơn hàng có mã: {orderId}");
            var orderPayments = await (from rv in _context.ReceiptVouchers
                join pm in _context.PaymentMethods on rv.PaymentMethodId equals pm.Id
                join e in _context.Employees on rv.EmployeeId equals e.Id
                where rv.OrderId == checkOrder.Id
                select new OrderPaymentsDto()
                {
                    Id = rv.Id,
                    Received = rv.Received,
                    PaymentDate = rv.ReceivedDate,
                    PaymentMethodName = pm.Name,
                    EmployeeName = e.Name
                }).ToListAsync();
            return new ApiResult<IEnumerable<OrderPaymentsDto>>(HttpStatusCode.OK,orderPayments);
        }
    }
}
