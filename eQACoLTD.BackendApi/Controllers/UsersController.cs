using eQACoLTD.Application.System.Account;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.User.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin",AuthenticationSchemes ="Bearer")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;   

        public UsersController(IUserService accountService,IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = accountService;
        }
        [HttpGet("{userName}/roles")]
        public async Task<IActionResult> GetUserRoles(string userName)
        {
            var result = await _userService.GetUserRolesAsync(userName);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut("{userName}/roles")]
        public async Task<IActionResult> UpdateUserRoles(string userName,
            [FromBody] UpdateUserRoleRequest request)
        {
            var result = await _userService.UpdateUserRolesAsync(userName,request);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserProfilesPaging(int pageIndex)
        {
            var result = await _userService.GetUserProfilePagingAsync(new PagingRequestBase()
            {
                PageIndex = pageIndex,
                PageSize = int.Parse(_configuration["PageSize"])
            });
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
