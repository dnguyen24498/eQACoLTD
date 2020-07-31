using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.User.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<UserProfileResponse>> GetUserProfileAsync();
    }
}
