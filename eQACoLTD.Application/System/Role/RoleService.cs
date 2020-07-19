using eQACoLTD.Application.Configurations;
using eQACoLTD.Data.Entities;
using eQACoLTD.Utilities.Exceptions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Role.Handlers;
using eQACoLTD.ViewModel.System.Role.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Role
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task DeleteRoleAsync(string roleId)
        {
            var checkRole = await _roleManager.FindByIdAsync(roleId);
            if (checkRole == null) throw new eQANotFoundException();
            await _roleManager.DeleteAsync(checkRole);
        }

        public async Task<ApiResult<RoleResponse>> GetRoleAsync(string roleId)
        {
            var checkRole = await _roleManager.FindByIdAsync(roleId);
            if (checkRole == null) return new ApiErrorResult<RoleResponse>($"Không tìm thấy quyền có ID:{roleId}");
            return new ApiSuccessResult<RoleResponse>(ObjectMapper.Mapper.Map<RoleResponse>(checkRole));
        }

        public async Task<ApiResult<List<RoleResponse>>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesConvert = new List<RoleResponse>();
            roles.ForEach(x =>
            {
                rolesConvert.Add(ObjectMapper.Mapper.Map<RoleResponse>(x));
            });
            return new ApiSuccessResult<List<RoleResponse>>(rolesConvert);
        }

        public async Task<ApiResult<string>> PostRoleAsync(CreateRoleRequest newRole)
        {
            var role = ObjectMapper.Mapper.Map<AppRole>(newRole);
            var createRoleResult = await _roleManager.CreateAsync(role);
            if (!createRoleResult.Succeeded) return new ApiErrorResult<string>("Có lỗi khi tạo mới quyền");
            var idRoleCreated = await _roleManager.GetRoleIdAsync(role);
            if(string.IsNullOrEmpty(idRoleCreated)) return new ApiErrorResult<string>("Có lỗi khi tạo mới quyền");
            return new ApiSuccessResult<string>(idRoleCreated);
        }

        public async Task<ApiResult<RoleResponse>> PutRoleAsync(string roleId, UpdateRoleRequest updateInfo)
        {
            var checkRole = await _roleManager.FindByIdAsync(roleId);
            if (checkRole == null) return new ApiErrorResult<RoleResponse>($"Không tìm thấy quyền có ID:{roleId}");
            checkRole = ObjectMapper.Mapper.Map(updateInfo,checkRole);
            var updateResult = await _roleManager.UpdateAsync(checkRole);
            if (!updateResult.Succeeded) return new ApiErrorResult<RoleResponse>("Có lỗi khi sửa quyền");
            var updatedRole = await _roleManager.FindByIdAsync(checkRole.Id.ToString());
            return new ApiSuccessResult<RoleResponse>(ObjectMapper.Mapper.Map<RoleResponse>(updatedRole));
        }
    }
}
