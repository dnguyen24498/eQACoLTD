using eQACoLTD.Application.Configurations;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.User.Queries;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.User
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ApiResult<string>> ChangeAccountPasswordAsync(string userName,ChangeAccountPasswordRequest request)
        {
            var checkUser = await _userManager.FindByNameAsync(userName);
            var checkOldPassword = await _userManager.CheckPasswordAsync(checkUser, request.OldPassword);
            if (!checkOldPassword) return new ApiErrorResult<string>("Mật khẩu hiện tại không đúng");
            var changePasswordResult = await _userManager.ChangePasswordAsync(checkUser, 
                request.OldPassword, request.NewPassword);
            if(!changePasswordResult.Succeeded) return new ApiErrorResult<string>("Có lỗi khi đổi mật khẩu");
            return new ApiSuccessResult<string>(checkUser.UserName);
        }

        public async Task<ApiResult<AccountProfileResponse>> GetAccountProfileAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) 
                return new ApiErrorResult<AccountProfileResponse>($"Không tìm thấy người dùng có tên {userName}");
            var userProfile = ObjectMapper.Mapper.Map<AccountProfileResponse>(user);
            return new ApiSuccessResult<AccountProfileResponse>(userProfile);
        }

        public async Task<ApiResult<AccountProfileResponse>> UpdateAccountProfileAsync(string userName,
            AccountProfileResponse request)
        {
            var checkUser = await _userManager.FindByNameAsync(userName);
            if (checkUser == null) 
                return new ApiErrorResult<AccountProfileResponse>($"Không tìm thấy người dùng có tên {userName}");
            checkUser=ObjectMapper.Mapper.Map(request, checkUser);
            var result = await _userManager.UpdateAsync(checkUser);
            if (!result.Succeeded) 
                return new ApiErrorResult<AccountProfileResponse>("Cập nhật thông tin không thành công");
            return new ApiSuccessResult<AccountProfileResponse>(ObjectMapper.Mapper.Map<AccountProfileResponse>(checkUser));
        }
    }
}
