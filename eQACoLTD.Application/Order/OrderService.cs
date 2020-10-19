using eQACoLTD.Data.DBContext;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using eQACoLTD.Application.Extensions;
using System.Net;
using Microsoft.EntityFrameworkCore;
using eQACoLTD.ViewModel.Order.Handlers;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Data.Entities;
using eQACoLTD.Application.Common;
namespace eQACoLTD.Application.Order
{
    public class OrderService : IOrderService
    {
        private readonly AppIdentityDbContext _context;
        public OrderService(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<string>> CreateOrderAsync(OrderForCreationDto creationDto,string employeeId)
        {
            try
            {
                var checkEmployee = await _context.Employees.FindAsync(employeeId);
                if(checkEmployee==null) return new ApiResult<string>(HttpStatusCode.NotFound);
                var sequenceNumber = await _context.Orders.CountAsync();
                var orderId = IdentifyGenerator.GenerateOrderId(sequenceNumber + 1);
                var order = ObjectMapper.Mapper.Map<Data.Entities.Order>(creationDto);
                var orderDetail = ObjectMapper.Mapper.Map<IEnumerable<OrderDetail>>(creationDto.ListOrderDetail);
                decimal totalAmount = 0;
                int checkIfAllService = 0;
                foreach (var od in orderDetail)
                {
                    od.Id = Guid.NewGuid().ToString("D");
                    od.OrderId = orderId;
                    totalAmount += od.Quantity * od.UnitPrice;
                    if (string.IsNullOrEmpty(od.ProductId))
                    {
                        od.ProductId = null;
                        checkIfAllService++;
                    }
                }
                order.Id = orderId;
                if (checkIfAllService == creationDto.ListOrderDetail.Count())
                {
                    order.TransactionStatusId = GlobalProperties.TradingTransactionId;
                }
                else
                {
                    order.TransactionStatusId = GlobalProperties.InventoryTransactionId;   
                }
                order.PaymentStatusId = GlobalProperties.UnpaidPaymentId;
                order.DateCreated = DateTime.Now;
                order.BranchId = checkEmployee.BranchId;
                order.EmployeeId = checkEmployee.Id;
                if (string.IsNullOrEmpty(order.DiscountType))
                {
                    order.DiscountType = "$";
                    order.DiscountValue = 0;
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

        public async Task<ApiResult<PagedResult<OrdersDto>>> GetWaitingOrderAsync(int pageIndex, int pageSize)
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
                orderby o.DateCreated descending
                where o.TransactionStatusId==GlobalProperties.WaitingTransactionId
                let hasPaid=_context.ReceiptVouchers.Where(x => x.OrderId == o.Id).Sum(x => x.Received)
                select new OrdersDto()
                {
                    OrderId=o.Id,
                    CustomerName=c.Name,
                    EmployeeName=e.Name,
                    DateCreated=o.DateCreated,
                    PaymentStatusName=ps.Name,
                    TransactionStatusName=ts.Name,
                    CustomerHasPaid = o.TotalAmount
                }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<OrdersDto>>(HttpStatusCode.OK,orders);
        }

        public async Task<ApiResult<string>> AcceptWaitingOrderAsync(string employeeId, string waitingOrderId,AcceptOrderDto orderDto)
        {
            var checkEmployee = await _context.Employees.FindAsync(employeeId);
            if(checkEmployee==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy nhân viên có mã: {employeeId}");
            var checkOrder = await _context.Orders.FindAsync(waitingOrderId);
            if(checkOrder==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy đơn hàng có mã: {waitingOrderId}");
            if (checkOrder.TransactionStatusId == GlobalProperties.WaitingTransactionId)
            {
                var totalAmount = await (from od in _context.OrderDetails
                    where od.OrderId == checkOrder.Id
                    select od.UnitPrice * od.Quantity).SumAsync();
                checkOrder.EmployeeId = checkEmployee.Id;
                checkOrder.BranchId = checkEmployee.BranchId;
                checkOrder.Description = orderDto.Description;
                checkOrder.TransactionStatusId = GlobalProperties.InventoryTransactionId;
                checkOrder.DiscountDescription = orderDto.DiscountDescription;
                checkOrder.DiscountValue = orderDto.DiscountValue;
                checkOrder.DiscountType = orderDto.DiscountType;
                checkOrder.TotalAmount = checkOrder.DiscountType == "%"
                    ? totalAmount - (totalAmount * checkOrder.DiscountValue / 100)
                    : totalAmount - checkOrder.DiscountValue;
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK)
                {
                    ResultObj = checkOrder.Id
                };
            }
            return new ApiResult<string>(HttpStatusCode.BadRequest,$"Đơn hàng phải trong trạng thái chờ");
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
                                                       join product in _context.Products on od.ProductId equals product.Id
                                                       into ProductsGroup
                                                       from p in ProductsGroup.DefaultIfEmpty()
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
                                   RestAmount=o.TotalAmount-hasPaid,
                                   CustomerId = c.Id
                               }).SingleOrDefaultAsync();
            if (orderDetails == null) return new ApiResult<OrderDto>(HttpStatusCode.NotFound, $"Không tìm thấy đơn hàng có mã: {orderId}");
            return new ApiResult<OrderDto>(HttpStatusCode.OK, orderDetails);
        }

        public async Task<ApiResult<PagedResult<OrdersDto>>> GetOrdersPagingAsync(string employeeId,int pageIndex, int pageSize)
        {
            var checkEmployee = await _context.Employees.FindAsync(employeeId);
            if (checkEmployee == null) return new ApiResult<PagedResult<OrdersDto>>(HttpStatusCode.NotFound);
            var orders = await (from o in _context.Orders
                          join customer in _context.Customers on o.CustomerId equals customer.Id
                          into CustomersGroup
                          from c in CustomersGroup.DefaultIfEmpty()
                          join employee in _context.Employees on o.EmployeeId equals employee.Id
                          into EmployeesGroup
                          from e in EmployeesGroup.DefaultIfEmpty()
                          join ps in _context.PaymentStatuses on o.PaymentStatusId equals ps.Id
                          join ts in _context.TransactionStatuses on o.TransactionStatusId equals ts.Id
                          orderby o.DateCreated descending 
                          where o.BranchId==checkEmployee.BranchId
                          let hasPaid=_context.ReceiptVouchers.Where(x => x.OrderId == o.Id).Sum(x => x.Received)
                          select new OrdersDto()
                          {
                              OrderId=o.Id,
                              CustomerName=c.Name,
                              EmployeeName=e.Name,
                              DateCreated=o.DateCreated,
                              PaymentStatusName=ps.Name,
                              TransactionStatusName=ts.Name,
                              CustomerHasPaid = o.TotalAmount
                          }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<OrdersDto>>(HttpStatusCode.OK, orders);
        }
    }
}
