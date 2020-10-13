using eQACoLTD.Data.DBContext;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using eQACoLTD.Application.Extensions;
using System.Net;
using Microsoft.EntityFrameworkCore;
using eQACoLTD.ViewModel.Order.Handlers;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Data.Entities;
using eQACoLTD.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.Application.Order
{
    public class OrderService : IOrderService
    {
        private readonly AppIdentityDbContext _context;
        public OrderService(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<string>> CreateOrderAsync(OrderForCreationDto creationDto)
        {
            try
            {
                var sequenceNumber = await _context.Orders.CountAsync();
                var orderId = IdentifyGenerator.GenerateOrderId(sequenceNumber + 1);
                var order = ObjectMapper.Mapper.Map<Data.Entities.Order>(creationDto);
                var orderDetail = ObjectMapper.Mapper.Map<IEnumerable<OrderDetail>>(creationDto.ListOrderDetail);
                var employee = await _context.Employees.Where(x => x.Id == creationDto.EmployeeId).SingleOrDefaultAsync();
                order.Id = orderId;
                order.TransactionStatusId = GlobalProperties.InventoryTransactionId;
                order.PaymentStatusId = GlobalProperties.UnpaidPaymentId;
                order.DateCreated = DateTime.Now;
                if (string.IsNullOrEmpty(order.BranchId)) order.BranchId = employee.BranchId;
                decimal totalAmount = 0;
                foreach (var od in orderDetail)
                {
                    od.Id = Guid.NewGuid().ToString("D");
                    od.OrderId = orderId;
                    totalAmount += od.Quantity * od.UnitPrice;
                }
                order.TotalAmount = totalAmount - (order.DiscountType == "%" ? totalAmount * order.DiscountValue / 100 : order.DiscountValue);
                order.OrderDetails = orderDetail.ToList();
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK) { ResultObj=orderId };
            }
            catch
            {
                return new ApiResult<string>(HttpStatusCode.InternalServerError, "Có lỗi khi tạo đơn hàng");
            }
        }

        public async Task<ApiResult<OrderDto>> GetOrderAsync(string orderId)
        {
            var hasPaid = _context.ReceiptVouchers.Where(x => x.OrderId == orderId).Sum(x => x.Received);
            var orderDetails = await (from o in _context.Orders
                               join c in _context.Customers on o.CustomerId equals c.Id
                               join ts in _context.TransactionStatuses on o.TransactionStatusId equals ts.Id
                               join ps in _context.PaymentStatuses on o.PaymentStatusId equals ps.Id
                               join b in _context.Branches on o.BranchId equals b.Id
                               join employee in _context.Employees on o.EmployeeId equals employee.Id
                               into EmployeesGroup
                               from e in EmployeesGroup.DefaultIfEmpty()
                               where o.Id==orderId
                               select new OrderDto()
                               {
                                   Id = o.Id,
                                   BranchName = b.Name,
                                   CustomerName = c.Name,
                                   DateCreated = o.DateCreated,
                                   Description = o.Description,
                                   DiscountDescription = o.DiscountDescription,
                                   DiscountType = o.DiscountType,
                                   DiscountValue = o.DiscountValue,
                                   EmployeeName = e.Name,
                                   PaymentStatusName = ps.Name,
                                   TransactionStatusName = ts.Name,
                                   OrderDetailsDtos = (from od in _context.OrderDetails
                                                       join p in _context.Products on od.ProductId equals p.Id
                                                       where od.OrderId == o.Id
                                                       select new OrderDetailsDto()
                                                       {
                                                           Id = od.Id,
                                                           ProductId = od.ProductId,
                                                           Quantity = od.Quantity,
                                                           ServiceName = od.ServiceName,
                                                           UnitPrice = od.UnitPrice,
                                                           ProductName = p.Name
                                                       }).ToList(),
                                   TotalAmount=o.TotalAmount,
                                   RestAmount=o.TotalAmount-hasPaid
                               }).SingleOrDefaultAsync();
            if (orderDetails == null) return new ApiResult<OrderDto>(HttpStatusCode.NotFound, $"Không tìm thấy đơn hàng có mã: {orderId}");
            return new ApiResult<OrderDto>(HttpStatusCode.OK, orderDetails);
        }

        public async Task<ApiResult<PagedResult<OrdersDto>>> GetOrdersPagingAsync(string branchId,int pageIndex, int pageSize)
        {
            var orders = await (from o in _context.Orders
                          join customer in _context.Customers on o.CustomerId equals customer.Id
                          into CustomersGroup
                          from c in CustomersGroup.DefaultIfEmpty()
                          join employee in _context.Employees on o.EmployeeId equals employee.Id
                          into EmployeesGroup
                          from e in EmployeesGroup.DefaultIfEmpty()
                          join ps in _context.PaymentStatuses on o.PaymentStatusId equals ps.Id
                          join ts in _context.TransactionStatuses on o.TransactionStatusId equals ts.Id
                          where o.BranchId==branchId
                          select new OrdersDto()
                          {
                              OrderId=o.Id,
                              CustomerName=c.Name,
                              EmployeeName=e.Name,
                              DateCreated=o.DateCreated,
                              PaymentStatusName=ps.Name,
                              TransactionStatusName=ts.Name
                          }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<OrdersDto>>(HttpStatusCode.OK, orders);
        }

        public async Task<ApiResult<string>> ExportStockOrderAsync(string orderId, OrderGoodsDeliveryNoteForCreationDto creationDto)
        {
            try
            {
                var checkOrder = await _context.Orders.FindAsync(orderId);
                if (checkOrder == null) return new ApiResult<string>(HttpStatusCode.NotFound, ($"Không tìm thấy đơn hàng có mã: {orderId}"));
                var goodsDeliveryNote = ObjectMapper.Mapper.Map<GoodsDeliveryNote>(creationDto);
                var sequencyNumber = await _context.GoodsDeliveryNotes.CountAsync();
                var goodsDeliveryNoteId = IdentifyGenerator.GenerateGoodsDeliveryNoteId(sequencyNumber + 1);
                goodsDeliveryNote.Id = goodsDeliveryNoteId;
                goodsDeliveryNote.OrderId = orderId;
                var goodsDeliveryNoteDetails = await (from od in _context.OrderDetails
                                                      where od.OrderId == checkOrder.Id
                                                      select new GoodsDeliveryNoteDetail()
                                                      {
                                                          Id = Guid.NewGuid().ToString("D"),
                                                          GoodsDeliveryNoteId = goodsDeliveryNoteId,
                                                          ProductId = od.ProductId,
                                                          Quantity = od.Quantity,
                                                          UnitPrice = od.UnitPrice
                                                      }).ToListAsync();
                foreach (var g in goodsDeliveryNoteDetails)
                {
                    var prod = await _context.Stocks.Where(x => x.ProductId == g.ProductId && x.WarehouseId == creationDto.WarehouseId).
                        SingleOrDefaultAsync();
                    if (prod == null)
                    {
                        checkOrder.TransactionStatusId = GlobalProperties.CancelTransactionId;
                        await _context.SaveChangesAsync();
                        return new ApiResult<string>(HttpStatusCode.NotFound, $"Sản phẩm không có trong kho");
                    }
                    prod.RealQuantity -= g.Quantity;
                    prod.AbleToSale -= g.Quantity;
                    checkOrder.TransactionStatusId = GlobalProperties.TradingTransactionId;
                }
                goodsDeliveryNote.GoodsDeliveryNoteDetails = goodsDeliveryNoteDetails;
                await _context.GoodsDeliveryNotes.AddAsync(goodsDeliveryNote);
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK) { ResultObj = goodsDeliveryNoteId };
            }
            catch
            {
                return new ApiResult<string>(HttpStatusCode.InternalServerError, "Có lỗi khi tạo phiếu xuất kho");
            }
        }

        public async Task<ApiResult<PagedResult<InventoryTransactionOrdersDto>>> GetExportOrdersPagingAsync(string branchId,int pageIndex, int pageSize)
        {
            var orders = await (from o in _context.Orders
                                join e in _context.Employees on o.EmployeeId equals e.Id
                                join ts in _context.TransactionStatuses on o.TransactionStatusId equals ts.Id
                                join b in _context.Branches on o.BranchId equals b.Id
                                where o.TransactionStatusId == GlobalProperties.InventoryTransactionId && o.BranchId == branchId
                                select new InventoryTransactionOrdersDto()
                                {
                                    BranchId = branchId,
                                    DateCreated = o.DateCreated,
                                    EmployeeName = e.Name,
                                    OrderId = o.Id,
                                    TransactionStatusName = ts.Name,
                                    BranchName=b.Name
                                }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<InventoryTransactionOrdersDto>>(HttpStatusCode.OK, orders);
        }

        public async Task<ApiResult<string>> AddOrderReceiptVoucherAsync(string orderId, OrderReceiptVoucherForCreationDto creationDto)
        {
            var checkOrder = await _context.Orders.FindAsync(orderId);
            if (checkOrder == null) return new ApiResult<string>(HttpStatusCode.NotFound, ($"Không tìm thấy đơn hàng có mã: {orderId}"));
            if (checkOrder.TransactionStatusId == GlobalProperties.TradingTransactionId)
            {
                var receiptVoucher = ObjectMapper.Mapper.Map<ReceiptVoucher>(creationDto);
                var sequenceNumber = await _context.ReceiptVouchers.CountAsync();
                var receiptVoucherId = IdentifyGenerator.GenerateReceiptVoucherId(sequenceNumber + 1);
                receiptVoucher.Id = receiptVoucherId;
                receiptVoucher.OrderId = checkOrder.Id;
                receiptVoucher.DateCreated = DateTime.Now;
                receiptVoucher.CustomerId = checkOrder.CustomerId;
                receiptVoucher.IsDelete = false;
                await _context.ReceiptVouchers.AddAsync(receiptVoucher);
                await _context.SaveChangesAsync();
                var totalPaid = await (from rv in _context.ReceiptVouchers
                                       where rv.OrderId == checkOrder.Id
                                       select rv.Received).SumAsync();
                if (totalPaid>=checkOrder.TotalAmount)
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
            return new ApiResult<string>(HttpStatusCode.BadRequest, "Không thể tạo phiếu thu cho đơn hàng đã hoàn thành.");
        }
    }
}
