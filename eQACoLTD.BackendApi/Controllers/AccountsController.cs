using eQACoLTD.Application.System.Account;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin",AuthenticationSchemes ="Bearer")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;   

        public AccountsController(IAccountService accountService,IConfiguration configuration)
        {
            _configuration = configuration;
            _accountService = accountService;
        }
        [HttpGet("{userName}/roles")]
        public async Task<IActionResult> GetAccountRoles(string userName)
        {
            var result = await _accountService.GetAccountRolesAsync(userName);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut("{userName}/roles/")]
        public async Task<IActionResult> UpdateRoles(string userName,
            [FromBody] UpdateAccountRoleRequest request)
        {
            var result = await _accountService.UpdateAccountRolesAsync(userName,request);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountProfilePaging(int pageIndex)
        {
            var result = await _accountService.GetAccountProfilePagingAsync(new PagingRequestBase()
            {
                PageIndex = pageIndex,
                PageSize = int.Parse(_configuration["PageSize"])
            });
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
