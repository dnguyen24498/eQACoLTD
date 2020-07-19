using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Role.Handlers;
using eQACoLTD.ViewModel.System.Role.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Role
{
    public interface IRoleService
    {
        Task<ApiResult<List<RoleResponse>>> GetRolesAsync();
        Task<ApiResult<RoleResponse>> GetRoleAsync(string roleId);
        Task<ApiResult<string>> PostRoleAsync(CreateRoleRequest newRole);
        Task DeleteRoleAsync(string roleId);
        Task<ApiResult<RoleResponse>> PutRoleAsync(string roleId, UpdateRoleRequest updateInfo);

    }
}
