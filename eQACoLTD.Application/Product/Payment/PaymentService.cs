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

        public async Task<ApiResult<bool>> IsPaidPurchaseOrder(string purchaseOrderId)
        {
            var checkPurchaseOrder =
                await _context.PurchaseOrders.Where(x => x.Id == purchaseOrderId).SingleOrDefaultAsync();
            if(checkPurchaseOrder==null) return new ApiResult<bool>(HttpStatusCode.NotFound,$"Không tìm thấy phiếu nhập hàng có mã: {purchaseOrderId}");
            var checkPaymentVoucher = await (from pv in _context.PaymentVouchers
                where pv.PurchaseOrderId == checkPurchaseOrder.Id
                select pv.Paid).SumAsync();
            if(checkPaymentVoucher==checkPurchaseOrder.TotalAmount) return new ApiResult<bool>(HttpStatusCode.OK,true);
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

        public async Task<ApiResult<IEnumerable<PurchaseOrderPaymentsDto>>> GetPurchaseOrderPaymentHistory(string purchaseOrderId)
        {
            var checkPurchaseOrder =
                await _context.PurchaseOrders.Where(x => x.Id == purchaseOrderId).SingleOrDefaultAsync();
            if(checkPurchaseOrder==null) return new ApiResult<IEnumerable<PurchaseOrderPaymentsDto>>(HttpStatusCode.NotFound,$"Không tìm thấy phiếu nhập hàng có mã: {purchaseOrderId}");
            var getPaymentHistories = await (from pv in _context.PaymentVouchers
                join pm in _context.PaymentMethods on pv.PaymentMethodId equals pm.Id
                join employee in _context.Employees on pv.EmployeeId equals employee.Id
                    into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                where pv.PurchaseOrderId == purchaseOrderId
                select new PurchaseOrderPaymentsDto()
                {
                    Id = pv.Id,
                    Paid = pv.Paid,
                    EmployeeName = e.Name,
                    PaymentDate = pv.PaymentDate,
                    PaymentMethodName = pm.Name
                }).ToListAsync();
            return new ApiResult<IEnumerable<PurchaseOrderPaymentsDto>>(HttpStatusCode.OK,getPaymentHistories);
        }

        public async Task<ApiResult<string>> CreatePaymentVoucherAsync(PaymentVoucherForCreationDto creationDto, string accountId)
        {
            var employee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(employee==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
            if(!string.IsNullOrEmpty(creationDto.CustomerId)&&!string.IsNullOrEmpty(creationDto.SupplierId)) 
                return new ApiResult<string>(HttpStatusCode.BadRequest,$"Một phiếu chi chỉ thuộc về một đối tượng.");
            if(string.IsNullOrEmpty(creationDto.CustomerId)&&string.IsNullOrEmpty(creationDto.SupplierId))
                return new ApiResult<string>(HttpStatusCode.BadRequest,$"Phiếu chi phải thuộc về một đối tượng");
            if (!string.IsNullOrEmpty(creationDto.CustomerId))
            {
                var checkCustomer = await _context.Customers.Where(x => x.Id == creationDto.CustomerId)
                    .SingleOrDefaultAsync();
                if(checkCustomer==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy khách hàng có mã: {creationDto.CustomerId}");
            }
            else
            {
                var checkSupplier = await _context.Suppliers.Where(x => x.Id == creationDto.SupplierId)
                    .SingleOrDefaultAsync();
                if(checkSupplier==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy nhà cung cấp có mã: {creationDto.SupplierId}");
            }
            if(creationDto.Paid<=0) return new ApiResult<string>(HttpStatusCode.BadRequest,$"Số tiền chi phải là một số dương");
            var sequenceNumber = await _context.PaymentVouchers.CountAsync();
            var paymentVoucherId = IdentifyGenerator.GeneratePaymentVoucherId(sequenceNumber + 1);
            var paymentVoucher=new PaymentVoucher()
            {
                Description = creationDto.Description,
                Id = paymentVoucherId,
                Paid = creationDto.Paid,
                BranchId = employee.BranchId,
                CustomerId = string.IsNullOrEmpty(creationDto.CustomerId)?null:creationDto.CustomerId,
                SupplierId = string.IsNullOrEmpty(creationDto.SupplierId)?null:creationDto.SupplierId,
                EmployeeId = employee.Id,
                DateCreated = DateTime.Now,
                IsDelete = false,
                PaymentDate = creationDto.PaymentDate,
                PaymentMethodId = creationDto.PaymentMethodId
            };
            await _context.PaymentVouchers.AddAsync(paymentVoucher);
            await _context.SaveChangesAsync();
            return new ApiResult<string>(HttpStatusCode.OK)
            {
                ResultObj = paymentVoucherId,
                Message = "Tạo phiếu chi thành công"
            };
        }

        public async Task<ApiResult<string>> CreateReceiptVoucherAsync(ReceiptVoucherForCreationDto creationDto, string accountId)
        {
            var employee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(employee==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
            if(!string.IsNullOrEmpty(creationDto.CustomerId)&&!string.IsNullOrEmpty(creationDto.SupplierId)) 
                return new ApiResult<string>(HttpStatusCode.BadRequest,$"Một phiếu thu chỉ thuộc về một đối tượng.");
            if(string.IsNullOrEmpty(creationDto.CustomerId)&&string.IsNullOrEmpty(creationDto.SupplierId))
                return new ApiResult<string>(HttpStatusCode.BadRequest,$"Phiếu thu phải thuộc về một đối tượng");
            if (!string.IsNullOrEmpty(creationDto.CustomerId))
            {
                var checkCustomer = await _context.Customers.Where(x => x.Id == creationDto.CustomerId)
                    .SingleOrDefaultAsync();
                if(checkCustomer==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy khách hàng có mã: {creationDto.CustomerId}");
            }
            else
            {
                var checkSupplier = await _context.Suppliers.Where(x => x.Id == creationDto.SupplierId)
                    .SingleOrDefaultAsync();
                if(checkSupplier==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy nhà cung cấp có mã: {creationDto.SupplierId}");
            }
            if(creationDto.Received<=0) return new ApiResult<string>(HttpStatusCode.BadRequest,$"Số tiền thu phải là một số dương");
            var sequenceNumber = await _context.ReceiptVouchers.CountAsync();
            var receiptVoucherId = IdentifyGenerator.GenerateReceiptVoucherId(sequenceNumber + 1);
            var receiptVoucher=new ReceiptVoucher()
            {
                Description = creationDto.Description,
                Id = receiptVoucherId,
                Received = creationDto.Received,
                BranchId = employee.BranchId,
                CustomerId = string.IsNullOrEmpty(creationDto.CustomerId)?null:creationDto.CustomerId,
                DateCreated = DateTime.Now,
                EmployeeId = employee.Id,
                IsDelete = false,
                PaymentMethodId = creationDto.PaymentMethodId,
                ReceivedDate = creationDto.ReceivedDate,
                SupplierId = string.IsNullOrEmpty(creationDto.SupplierId)?null:creationDto.SupplierId
            };
            await _context.ReceiptVouchers.AddAsync(receiptVoucher);
            await _context.SaveChangesAsync();
            return new ApiResult<string>(HttpStatusCode.OK)
            {
                ResultObj = receiptVoucherId,
                Message = "Tạo phiếu thu thành công"
            };
        }

        public async Task<ApiResult<PagedResult<PaymentVouchersDto>>> GetPaymentVouchersPagingAsync(int pageIndex, int pageSize, string accountId)
        {
            var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(checkEmployee==null) return new ApiResult<PagedResult<PaymentVouchersDto>>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
            var paymentVouchers = await (from pv in _context.PaymentVouchers
                    join employee in _context.Employees on pv.EmployeeId equals employee.Id
                        into EmployeeGroup
                    from e in EmployeeGroup.DefaultIfEmpty()
                    join customer in _context.Customers on pv.CustomerId equals customer.Id
                        into CustomerGroup
                    from c in CustomerGroup.DefaultIfEmpty()
                    join supplier in _context.Suppliers on pv.SupplierId equals supplier.Id
                        into SupplierGroup
                    from s in SupplierGroup.DefaultIfEmpty()
                    join b in _context.Branches on pv.BranchId equals b.Id
                    join pm in _context.PaymentMethods on pv.PaymentMethodId equals pm.Id
                    where pv.BranchId == checkEmployee.BranchId
                    select new PaymentVouchersDto()
                    {
                        Id = pv.Id,
                        Paid = pv.Paid,
                        BranchName = b.Name,
                        PaymentDate = pv.PaymentDate,
                        PaymentMethodName = pm.Name,
                        PersonName = string.IsNullOrEmpty(pv.SupplierId)
                            ? $"Khách hàng {c.Name}"
                            : $"Nhà cung cấp {s.Name}",
                        EmployeeName = e.Name
                    }
                ).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<PaymentVouchersDto>>(HttpStatusCode.OK,paymentVouchers);
        }

        public async Task<ApiResult<PagedResult<ReceiptVouchersDto>>> GetReceiptVoucherPagingAsync(int pageIndex, int pageSize, string accountId)
        {
            var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(checkEmployee==null) return new ApiResult<PagedResult<ReceiptVouchersDto>>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
            var receiptVouchers = await (from rv in _context.ReceiptVouchers
                    join employee in _context.Employees on rv.EmployeeId equals employee.Id
                        into EmployeeGroup
                    from e in EmployeeGroup.DefaultIfEmpty()
                    join customer in _context.Customers on rv.CustomerId equals customer.Id
                        into CustomerGroup
                    from c in CustomerGroup.DefaultIfEmpty()
                    join supplier in _context.Suppliers on rv.SupplierId equals supplier.Id
                        into SupplierGroup
                    from s in SupplierGroup.DefaultIfEmpty()
                    join b in _context.Branches on rv.BranchId equals b.Id
                    join pm in _context.PaymentMethods on rv.PaymentMethodId equals pm.Id
                    where rv.BranchId == checkEmployee.BranchId
                    select new ReceiptVouchersDto()
                    {
                        Id = rv.Id,
                        Received = rv.Received,
                        BranchName = b.Name,
                        PaymentMethodName = pm.Name,
                        ReceivedDate = rv.ReceivedDate,
                        PersonName = string.IsNullOrEmpty(rv.CustomerId)
                            ? $"Nhà cung cấp {s.Name}"
                            : $"Khách hàng {c.Name}",
                        EmployeeName = e.Name
                    }
                ).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<ReceiptVouchersDto>>(HttpStatusCode.OK,receiptVouchers);
        }
    }
}
