using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Account
{
    public interface IAccountService
    {
        public Task<ApiResult<PagedResult<AccountResponse>>> GetAccountsPagingAsync(int pageIndex);
        public Task<ApiResult<AccountDetailResponse>> GetAccountDetailAsync(Guid userId);
        public Task<ApiResult<List<AccountRolesResponse>>> GetAccountRolesAsync(Guid userId);
        public Task<ApiResult<List<AccountRolesResponse>>> GetAccountNotInRolesAsync(Guid userId);
        public Task DeleteAccountRoleAsync(Guid userId, Guid roleId);
        public Task<ApiResult<string>> AddAccountRoleAsync(Guid userId, Guid roleId);
    }
}
