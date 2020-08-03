using eQACoLTD.Application.System.User;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService userService)
        {
            _accountService = userService;
        }
        [HttpGet("profile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAccountProfile()
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _accountService.GetAccountProfileAsync(userName);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut("profile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateAccountProfile([FromBody] AccountProfileResponse updateInfo)
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _accountService.UpdateAccountProfileAsync(userName,updateInfo);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("change-password")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeAccountPasswordRequest request)
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _accountService.ChangeAccountPasswordAsync(userName, request);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
