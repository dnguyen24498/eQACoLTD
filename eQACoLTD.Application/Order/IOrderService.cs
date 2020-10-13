using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Handlers;
using eQACoLTD.ViewModel.Order.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Order
{
    public interface IOrderService
    {
        Task<ApiResult<PagedResult<OrdersDto>>> GetOrdersPagingAsync(string employeeId,int pageIndex, int pageSize);
        Task<ApiResult<OrderDto>> GetOrderAsync(string orderId);
        Task<ApiResult<string>> CreateOrderAsync(OrderForCreationDto creationDto);
    }
}
