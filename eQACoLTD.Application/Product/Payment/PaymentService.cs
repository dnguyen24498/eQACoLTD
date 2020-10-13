using eQACoLTD.Application.Common;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.DBContext;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Payment.Handlers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly AppIdentityDbContext _context;
        public PaymentService(AppIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<string>> OrderReceiveAsync(string employeeId,string orderId, OrderPaymenForCreationDto creationDto)
        {
            var checkEmployee = await _context.Employees.FindAsync(employeeId);
            if (checkEmployee == null) return new ApiResult<string>(HttpStatusCode.NotFound);
            var checkOrder = await _context.Orders.FindAsync(orderId);
            if (checkOrder == null) return new ApiResult<string>(HttpStatusCode.NotFound, ($"Không tìm thấy đơn hàng có mã: {orderId}"));
            if (checkOrder.TransactionStatusId == GlobalProperties.TradingTransactionId)
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
                    if (totalPaid >= checkOrder.TotalAmount)
                    {
                        checkOrder.TransactionStatusId = GlobalProperties.FinishedTransactionId;
                        checkOrder.PaymentStatusId = GlobalProperties.PaidPaymentId;
                    }
                    else if (totalPaid < checkOrder.TotalAmount)
                    {
                        checkOrder.TransactionStatusId = GlobalProperties.TradingTransactionId;
                        checkOrder.PaymentStatusId = GlobalProperties.PartialPaymentId;
                    }
                    await _context.SaveChangesAsync();
                    return new ApiResult<string>(HttpStatusCode.OK) { ResultObj = receiptVoucherId };
                }
                return new ApiResult<string>(HttpStatusCode.Forbidden, $"Tài khoản đăng nhập hiện tại không có quyền chỉnh sửa phiếu thu tại chi nhánh này");

            }
            return new ApiResult<string>(HttpStatusCode.BadRequest, "Chỉ được tạo phiếu thu cho đơn hàng đang trọng trạng thái giao dịch.");
        }

        public async Task<ApiResult<string>> PurchaseOrderPaymentAsync(string employeeId, string purchaseOrderId, 
            PurchaseOrderPaymentForCreationDto creationDto)
        {
            var checkEmployee = await _context.Employees.FindAsync(employeeId);
            if (checkEmployee == null) return new ApiResult<string>(HttpStatusCode.NotFound);
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
                return new ApiResult<string>(HttpStatusCode.OK) { ResultObj = paymentVoucherId };

            }
            return new ApiResult<string>(HttpStatusCode.Forbidden, $"Tài khoản hiện tại không có quyền chỉnh sửa phiếu chi tại chi nhánh này");
        }
    }
}
