using Dapper;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.DBContext;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Account
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountService(IConfiguration configuration,RoleManager<AppRole> roleManager,
            UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ApiResult<string>> AddAccountRoleAsync(Guid userId, Guid roleId)
        {
            var checkUser = await _userManager.FindByIdAsync(userId.ToString("D"));
            var checkRole = await _roleManager.FindByIdAsync(roleId.ToString("D"));
            if (checkUser == null || checkRole == null) return new ApiErrorResult<string>();
            var result = await _userManager.AddToRoleAsync(checkUser, checkRole.Name);
            if (!result.Succeeded) return new ApiErrorResult<string>();
            return new ApiSuccessResult<string>(checkRole.Id.ToString());

        }

        public async Task DeleteAccountRoleAsync(Guid userId, Guid roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString("D"));
            var role = await _roleManager.FindByIdAsync(roleId.ToString("D"));
            if (user == null || role==null) return;
            await _userManager.RemoveFromRoleAsync(user, role.Name);
        }

        public async Task<ApiResult<AccountDetailResponse>> GetAccountDetailAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var checkUser = await _userManager.FindByIdAsync(userId.ToString());
                if (checkUser == null) return new ApiErrorResult<AccountDetailResponse>();
                var account = await connection.QueryFirstOrDefaultAsync<AccountDetailResponse>
                    (@"EXEC prGetAccountDetail @userId=@UserId", new { UserId = userId.ToString("D") });
                account.InRoles = (List<AccountRolesResponse>)await connection.QueryAsync<AccountRolesResponse>
                    (@"EXEC prGetAccountRoles @userId=@UserId", new { UserId = userId.ToString("D")});
                return new ApiSuccessResult<AccountDetailResponse>(account);
            }
        }

        public async Task<ApiResult<List<AccountRolesResponse>>> GetAccountNotInRolesAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var user = _userManager.FindByIdAsync(userId.ToString());
                if (user == null) return new ApiErrorResult<List<AccountRolesResponse>>();
                var notInRoles = await connection.QueryAsync<AccountRolesResponse>
                    (@"EXEC prGetAccountNotInRoles @userId=@UserId", new { UserId = userId });
                return new ApiSuccessResult<List<AccountRolesResponse>>(notInRoles.ToList());
            }

        }

        public Task<ApiResult<List<AccountRolesResponse>>> GetAccountRolesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PagedResult<AccountResponse>>> GetAccountsPagingAsync(int pageIndex)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var accounts = await connection.QueryAsync<AccountResponse>
                    (@"EXEC prGetAccountsPaging @pageNumber=@PageNumber,@rowsOfPage=@RowsOfPage",
                    new { PageNumber=pageIndex,RowsOfPage=int.Parse(_configuration["PageSize"]) });
                return new ApiSuccessResult<PagedResult<AccountResponse>>
                    (accounts.MapPage(pageIndex, int.Parse(_configuration["PageSize"]),accounts.ElementAt(0).TotalRecord));
            }
        }
    }
}
