using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.Role.Queries;
using eQACoLTD.ViewModel.System.User.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public AccountService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResult<PagedResult<UserProfileResponse>>> GetAccountProfilePagingAsync(PagingRequestBase pagingRequest)
        {
            if (pagingRequest == null)
                return new ApiErrorResult<PagedResult<UserProfileResponse>>("Nhập sai dữ liệu");
            var users = await _userManager.Users
                .GetPagedAsync<AppUser, UserProfileResponse>(pagingRequest.PageIndex, pagingRequest.PageSize);
            return new ApiSuccessResult<PagedResult<UserProfileResponse>>(users);
        }

        public async Task<ApiResult<AccountRolesVM>> GetAccountRolesAsync(string userName)
        {
            var checkUser = await _userManager.FindByNameAsync(userName);
            if (checkUser == null) return new
                       ApiErrorResult<AccountRolesVM>($"Không tìm thấy người dùng có tên: {userName}");
            var userResponse = new AccountRolesVM();
            userResponse.UserName = checkUser.UserName;
            userResponse.InRoles = (List<string>)await _userManager.GetRolesAsync(checkUser);
            var allRole = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
            userResponse.NotInRoles = (List<string>)allRole.Except(userResponse.InRoles).ToList();
            return new ApiSuccessResult<AccountRolesVM>(userResponse);
        }

        public async Task<ApiResult<string>> UpdateAccountRolesAsync(string userName,
            UpdateAccountRoleRequest request)
        {
            var checkUser = await _userManager.FindByNameAsync(userName);
            AppRole roleIdTemp;
            if (request.AddRoleNames != null && request.AddRoleNames.Count>0)
            {
                foreach (var role in request.AddRoleNames)
                {
                    roleIdTemp = await _roleManager.FindByNameAsync(role);
                    if (roleIdTemp != null)
                    {
                        await _userManager.AddToRoleAsync(checkUser, role);
                    }
                }
            }

            if (request.DeleteRoleNames != null && request.DeleteRoleNames.Count>0)
            {
                foreach (var role in request.DeleteRoleNames)
                {
                    roleIdTemp = await _roleManager.FindByNameAsync(role);
                    if (roleIdTemp != null)
                    {
                        await _userManager.RemoveFromRoleAsync(checkUser, role);
                    }
                }
            }
            return new ApiSuccessResult<string>(checkUser.UserName);
        }
    }
}
