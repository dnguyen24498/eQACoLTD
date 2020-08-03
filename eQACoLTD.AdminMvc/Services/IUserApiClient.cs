using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.User.Handlers;
using eQACoLTD.ViewModel.System.User.Queries;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<PagedResult<UserProfileResponse>>> GetUserProfilesPagingAsync(int page);
        Task<ApiResult<UserRolesResponse>> GetUserRolesAsync(string userName);
        Task<ApiResult<string>> UpdateUserRolesAsync(string userName,UpdateUserRoleRequest request);
    }
}
