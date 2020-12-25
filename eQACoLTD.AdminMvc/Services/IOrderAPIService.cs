using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IOrderAPIService
    {
        Task<ApiResult<PagedResult<OrdersDto>>> GetOrdersPagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<OrderDto>> GetOrderAsync(string orderId);
        Task<ApiResult<PagedResult<OrdersDto>>> GetWaitingOrderAsync(int pageIndex, int pageSize);
        Task<ApiResult<WaitingOrderDto>> GetWaitingOrderDetailAsync(string orderId);
        Task<ApiResult<string>> AcceptWaitingOrderAsync(string waitingOrderId);
        Task<ApiResult<string>> CancelWaitingOrderAsync(string waitingOrderId);
    }
}