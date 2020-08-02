using eQACoLTD.Application.System.Role;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Role.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin",AuthenticationSchemes = "Bearer")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IConfiguration _configuration;
        public RolesController(IRoleService roleService,IConfiguration configuration)
        {
            _roleService = roleService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles(int pageIndex)
        {
            var response = await _roleService.GetRolesAsync(new PagingRequestBase()
            {
                PageIndex = pageIndex,
                PageSize= int.Parse(_configuration["PageSize"])
            });
            if (!response.IsSuccess) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole(string roleId)
        {
            var response = await _roleService.GetRoleAsync(roleId);
            if (!response.IsSuccess) return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] CreateRoleRequest newRole)
        {
            var response = await _roleService.PostRoleAsync(newRole);
            if (!response.IsSuccess) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpPut("{roleId}")]
        public async Task<IActionResult> PutRole(string roleId,[FromBody] UpdateRoleRequest infoRole)
        {
            var response = await _roleService.PutRoleAsync(roleId,infoRole);
            if (!response.IsSuccess) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            await _roleService.DeleteRoleAsync(roleId);
            return Ok();
        }
    }
}
