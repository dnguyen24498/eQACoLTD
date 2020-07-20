using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Account
{
    public interface IAccountService
    {
        Task<ApiResult<GetAccountRoleResponse>> GetAccountRolesAsync(string userName);
        Task<ApiResult<string>> UpdateAccountRolesAsync(string userName,UpdateAccountRoleRequest request);
    }
}
