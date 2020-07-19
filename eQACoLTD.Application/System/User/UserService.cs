using eQACoLTD.Application.Configurations;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.User.Handlers;
using eQACoLTD.ViewModel.System.User.Queries;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ApiResult<string>> ChangeUserPasswordAsync(string userName,ChangeUserPasswordRequest request)
        {
            var checkUser = await _userManager.FindByNameAsync(userName);
            var checkOldPassword = await _userManager.CheckPasswordAsync(checkUser, request.OldPassword);
            if (!checkOldPassword) return new ApiErrorResult<string>("Mật khẩu hiện tại không đúng");
            var changePasswordResult = await _userManager.ChangePasswordAsync(checkUser, request.OldPassword, request.NewPassword);
            if(!changePasswordResult.Succeeded) return new ApiErrorResult<string>("Có lỗi khi đổi mật khẩu");
            return new ApiSuccessResult<string>(checkUser.UserName);
        }

        public async Task<ApiResult<UserProfileResponse>> GetUserProfileAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return new ApiErrorResult<UserProfileResponse>($"Không tìm thấy người dùng có tên {userName}");
            return new ApiSuccessResult<UserProfileResponse>(ObjectMapper.Mapper.Map<UserProfileResponse>(user));
        }

        public async Task<ApiResult<UserProfileResponse>> UpdateUserProfileAsync(string userName,UserProfileResponse request)
        {
            var checkUser = await _userManager.FindByNameAsync(userName);
            if (checkUser == null) return new ApiErrorResult<UserProfileResponse>($"Không tìm thấy người dùng có tên {userName}");
            checkUser=ObjectMapper.Mapper.Map(request, checkUser);
            var result = await _userManager.UpdateAsync(checkUser);
            if (!result.Succeeded) return new ApiErrorResult<UserProfileResponse>("Cập nhật thông tin không thành công");
            return new ApiSuccessResult<UserProfileResponse>(ObjectMapper.Mapper.Map<UserProfileResponse>(checkUser));
        }
    }
}
