using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;

namespace eQACoLTD.ClientMvc.Services
{
    public interface IAccountAPIService
    {
        Task<ApiResult<int>> AddProductToCart(string productId);
        Task<ApiResult<CartDto>> GetCart();
        Task<ApiResult<string>> CreateOrderFromCartAsync();
        Task<ApiResult<CustomerInfo>> GetCurrentAccountInfo();
        Task<ApiResult<PagedResult<AccountOrdersDto>>> GetAccountOrders(int pageIndex, int pageSize);
        Task<ApiResult<string>> CreateOrderForUnknownUser(CartDto cartDto);
    }
}