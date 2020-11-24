using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Handlers;
using eQACoLTD.ViewModel.Order.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.System.Account.Queries;

namespace eQACoLTD.Application.Order
{
    public interface IOrderService
    {
        Task<ApiResult<PagedResult<OrdersDto>>> GetOrdersPagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<OrderDto>> GetOrderAsync(string orderId);
        Task<ApiResult<string>> CreateOrderAsync(OrderForCreationDto creationDto,string accountId);
        Task<ApiResult<PagedResult<OrdersDto>>> GetWaitingOrderAsync(int pageIndex, int pageSize);
        Task<ApiResult<string>> AcceptWaitingOrderAsync(string accountId,string waitingOrderId);
        Task<ApiResult<WaitingOrderDto>> GetWaitingOrderDetailAsync(string orderId);
        Task<ApiResult<string>> CancelWaitingOrderAsync(string orderId);

        Task<ApiResult<string>> CreateShippingOrder(string orderId, ShippingOrderDto shippingOrderDto,
            string accountId);
        Task<ApiResult<string>> CreateOrderForUnknownUser(CartDto cartDto);
    }
}
