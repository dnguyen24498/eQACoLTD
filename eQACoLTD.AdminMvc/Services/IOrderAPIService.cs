using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IOrderAPIService
    {
        Task<ApiResult<PagedResult<OrdersDto>>> GetOrdersPagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<OrderDto>> GetOrderAsync(string orderId);
    }
}