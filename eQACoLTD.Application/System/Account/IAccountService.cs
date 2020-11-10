using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;

namespace eQACoLTD.Application.System.Account
{
    public interface IAccountService
    {
        Task<ApiResult<PagedResult<AccountsDto>>> GetAccountsPagingAsync(int pageIndex,int pageSize);
        Task<ApiResult<AccountDto>> GetAccountAsync(Guid userId);
        Task<ApiResult<Guid>> AddRoleAsync(Guid userId, Guid roleId);
        Task<ApiResult<Guid>> RemoveRoleAsync(Guid userId, Guid roleId);
        Task<ApiResult<IEnumerable<AccountRolesDto>>> NotInRolesAsync(Guid userId);
        Task<ApiResult<int>> AddProductToCart(string customerId, string productId);
        Task<ApiResult<CartDto>> GetCart(string customerId);
        Task<ApiResult<string>> DeleteProductFromCart(string customerId, string productId);
        Task<ApiResult<AccountInfo>> GetCurrentAccountInfo(string accountId);
        Task<ApiResult<string>> CreateOrderFromCartAsync(string customerId);
        Task<ApiResult<string>> UpdateAccountInfo(AccountForUpdateDto updateDto,string accountId);
    }
}