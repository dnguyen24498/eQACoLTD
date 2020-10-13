using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eQACoLTD.Data.DBContext;
using System.Linq;
using System.Net;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.Entities;
using eQACoLTD.Utilities.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eQACoLTD.Application.System.Account
{
    public class AccountService:IAccountService
    {
        private readonly AppIdentityDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountService(AppIdentityDbContext context,ILoggerManager loggerManager,
            RoleManager<AppRole> roleManager,UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<ApiResult<PagedResult<AccountsDto>>> GetAccountsPagingAsync(int pageIndex,int pageSize)
        {
            var accounts = await (from au in _context.AppUsers
                join emp in _context.Employees on au.Id equals emp.AppuserId
                into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                join cus in _context.Customers on au.Id equals cus.AppUserId
                into CustomerGroup
                from c in CustomerGroup.DefaultIfEmpty()
                join sup in _context.Suppliers on au.Id equals sup.AppUserId
                into SupplierGroup
                from s in SupplierGroup.DefaultIfEmpty()
                select new AccountsDto()
                {
                    Id = au.Id,
                    UserName = au.UserName,
                    Email = au.Email,
                    EmployeeName = e.Name,
                    CustomerName = c.Name,
                    SupplierName = s.Name,
                    PhoneNumber = au.PhoneNumber
                }).GetPagedAsync(pageIndex,pageSize);
            return new ApiResult<PagedResult<AccountsDto>>(HttpStatusCode.OK,accounts);
        }

        public async Task<ApiResult<AccountDto>> GetAccountAsync(Guid userId)
        {
            var checkAccount = await _userManager.FindByIdAsync(userId.ToString(("D")));
            if (checkAccount == null) 
                return new ApiResult<AccountDto>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
            var roles = await _userManager.GetRolesAsync(checkAccount);
            var accountRoles = new List<AccountRolesDto>();
            foreach (var r in roles)
            {
                var role = await _roleManager.FindByNameAsync(r);
                if(role!=null) accountRoles.Add(ObjectMapper.Mapper.Map<AppRole,AccountRolesDto>(role));
            }
            var accountDetail = await (from au in _context.AppUsers
                join emp in _context.Employees on au.Id equals emp.AppuserId
                    into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                join cus in _context.Customers on au.Id equals cus.AppUserId
                    into CustomerGroup
                from c in CustomerGroup.DefaultIfEmpty()
                join sup in _context.Suppliers on au.Id equals sup.AppUserId
                    into SupplierGroup
                from s in SupplierGroup.DefaultIfEmpty()
                where au.Id==userId
                select new AccountDto()
                {
                    Id = au.Id,
                    Address = e.Address != null
                        ? e.Address
                        : (c.Address != null ? c.Address : (s.Address != null ? s.Address : "")),
                    Email = au.Email,
                    Name = e.Name != null ? e.Name : (c.Name != null ? c.Name : (s.Name != null ? s.Name : "")),
                    PhoneNumber = au.PhoneNumber != null
                        ? au.PhoneNumber
                        : (e.PhoneNumber != null
                            ? e.PhoneNumber
                            : (c.PhoneNumber != null
                                ? c.PhoneNumber
                                : (
                                    s.PhoneNumber != null ? s.PhoneNumber : ""))),
                    UserName = au.UserName,
                    InRoles = accountRoles
                }).SingleOrDefaultAsync();
            return new ApiResult<AccountDto>(HttpStatusCode.OK,accountDetail);
        }

        public async Task<ApiResult<Guid>> AddRoleAsync(Guid userId, Guid roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString("D"));
            if (user == null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
            var role = await _roleManager.FindByIdAsync(roleId.ToString("D"));
            if(role==null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy quyền có mã: {roleId}");
            var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
            if(isInRole) return new ApiResult<Guid>(HttpStatusCode.NotModified,"Tài khoản đã có quyền này");
            var result=await _userManager.AddToRoleAsync(user, role.Name);
            if(result.Succeeded)
                return new ApiResult<Guid>(HttpStatusCode.OK,roleId);
            return new ApiResult<Guid>(HttpStatusCode.NotModified,$"Có lỗi khi thêm quyền");
        }

        public async Task<ApiResult<Guid>> RemoveRoleAsync(Guid userId, Guid roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString("D"));
            if (user == null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
            var role = await _roleManager.FindByIdAsync(roleId.ToString("D"));
            if(role==null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy quyền có mã: {roleId}");
            var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
            if(!isInRole)  return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Tài khoản không có quyền này");
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if(result.Succeeded)
                return new ApiResult<Guid>(HttpStatusCode.OK,roleId);
            return new ApiResult<Guid>(HttpStatusCode.NotModified,$"Có lỗi khi xóa quyền");
        }

        public async Task<ApiResult<IEnumerable<AccountRolesDto>>> NotInRolesAsync(Guid userId)
        {
            var checkUser = await _userManager.FindByIdAsync(userId.ToString("D"));
            if(checkUser==null) 
                return new ApiResult<IEnumerable<AccountRolesDto>>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
            var roles = await _userManager.GetRolesAsync(checkUser);
            var rolesConvert = new List<AppRole>();
            foreach (var r in roles)
            {
                var findRole = await _roleManager.FindByNameAsync(r);
                if(findRole!=null) rolesConvert.Add(findRole);
            }

            var notInRoles = (await _roleManager.Roles.ToListAsync()).Except(rolesConvert).ToList();
            return new ApiResult<IEnumerable<AccountRolesDto>>(HttpStatusCode.OK,
                ObjectMapper.Mapper.Map<List<AppRole>,List<AccountRolesDto>>(notInRoles));
        }
    }
}