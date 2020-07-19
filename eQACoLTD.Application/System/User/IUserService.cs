using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.User.Handlers;
using eQACoLTD.ViewModel.System.User.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.User
{
    public interface IUserService
    {
        Task<ApiResult<UserProfileResponse>> GetUserProfileAsync(string userName);
        Task<ApiResult<string>> ChangeUserPasswordAsync(string userName,ChangeUserPasswordRequest request);
        Task<ApiResult<UserProfileResponse>> UpdateUserProfileAsync(string userName,UserProfileResponse request);
    }
}
