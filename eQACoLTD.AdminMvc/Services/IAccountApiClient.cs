using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.User.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IAccountApiClient
    {
        Task<ApiResult<PagedResult<UserProfileResponse>>> GetAccountProfilePagingAsync(int page);
        Task<ApiResult<AccountRolesVM>> GetAccountRolesAsync(string userName);
        Task<ApiResult<string>> UpdateAccountRolesAsync(string userName,UpdateAccountRoleRequest request);
    }
}
