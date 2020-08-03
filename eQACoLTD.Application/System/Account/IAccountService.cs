﻿using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.User.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Account
{
    public interface IAccountService
    {
        Task<ApiResult<AccountRolesResponse>> GetAccountRolesAsync(string userName);
        Task<ApiResult<string>> UpdateAccountRolesAsync(string userName,UpdateAccountRoleRequest request);
        Task<ApiResult<PagedResult<UserProfileResponse>>> GetAccountProfilePagingAsync(PagingRequestBase pagingRequest);
    }
}
