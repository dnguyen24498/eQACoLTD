using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.User.Handlers;
using eQACoLTD.ViewModel.System.User.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Account
{
    public interface IUserService
    {
        Task<ApiResult<UserRolesResponse>> GetUserRolesAsync(string userName);
        Task<ApiResult<string>> UpdateUserRolesAsync(string userName,UpdateUserRoleRequest request);
        Task<ApiResult<PagedResult<UserProfileResponse>>> GetUserProfilePagingAsync(PagingRequestBase pagingRequest);
    }
}
