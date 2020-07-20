using eQACoLTD.Application.Configurations;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.Role.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

        public async Task<ApiResult<GetAccountRoleResponse>> GetAccountRolesAsync(string userName)
        {
            var checkUser = await _userManager.FindByNameAsync(userName);
            if (checkUser == null) return new
                       ApiErrorResult<GetAccountRoleResponse>($"Không tìm thấy người dùng có tên: {userName}");
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(checkUser);
            var userResponse = new GetAccountRoleResponse();
            userResponse.UserName = checkUser.UserName;
            userResponse.InRoles = new List<RoleResponse>();
            userResponse.NotInRoles = new List<RoleResponse>();
            var roleTemp = new AppRole();
            foreach (var role in userRoles)
            {
                roleTemp = await _roleManager.FindByNameAsync(role);
                if (await _roleManager.FindByNameAsync(role) != null) 
                    userResponse.InRoles.Add(ObjectMapper.Mapper.Map<RoleResponse>(roleTemp));
            }
            userResponse.NotInRoles = ObjectMapper.Mapper.Map<List<AppRole>, List<RoleResponse>>(roles).
                Except(userResponse.InRoles).ToList();
            return new ApiSuccessResult<GetAccountRoleResponse>(userResponse);
        }

        public async Task<ApiResult<string>> UpdateAccountRolesAsync(string userName,
            UpdateAccountRoleRequest request)
        {
            var checkUser = await _userManager.FindByNameAsync(userName);
            AppRole roleIdTemp;
            if (request.AddRoleIds != null && request.AddRoleIds.Count>0)
            {
                foreach (var role in request.AddRoleIds)
                {
                    roleIdTemp = await _roleManager.FindByNameAsync(role);
                    if (roleIdTemp != null)
                    {
                        await _userManager.AddToRoleAsync(checkUser, role);
                    }
                }
            }

            if (request.DeleteRoleIds != null && request.AddRoleIds.Count>0)
            {
                foreach (var role in request.DeleteRoleIds)
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
