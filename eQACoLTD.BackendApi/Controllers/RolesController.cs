using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.Application.System.Role;
using eQACoLTD.ViewModel.System.Role.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin",AuthenticationSchemes = "Bearer")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var response = await _roleService.GetRolesAsync();
            if (!response.IsSuccess) return Ok(response.Message);
            return Ok(response.ResultObj);
        }
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole(string roleId)
        {
            var response = await _roleService.GetRoleAsync(roleId);
            if (!response.IsSuccess) return Ok(response.Message);
            return Ok(response.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] CreateRoleRequest newRole)
        {
            var response = await _roleService.PostRoleAsync(newRole);
            if (!response.IsSuccess) return Ok(response.Message);
            return Ok(response.ResultObj);
        }
        [HttpPut("{roleId}")]
        public async Task<IActionResult> PutRole(string roleId,[FromBody] UpdateRoleRequest infoRole)
        {
            var response = await _roleService.PutRoleAsync(roleId,infoRole);
            if (!response.IsSuccess) return Ok(response.Message);
            return Ok(response.ResultObj);
        }
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            await _roleService.DeleteRoleAsync(roleId);
            return Ok();
        }
    }
}
