using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.Application.System.Account;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin",AuthenticationSchemes ="Bearer")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("{userName}/roles")]
        public async Task<IActionResult> GetAccountRoles(string userName)
        {
            var result = await _accountService.GetAccountRolesAsync(userName);
            if (!result.IsSuccess) return Ok(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("{userName}/roles/update")]
        public async Task<IActionResult> UpdateRoles(string userName,
            [FromBody] UpdateAccountRoleRequest request)
        {
            var result = await _accountService.UpdateAccountRolesAsync(userName,request);
            if (!result.IsSuccess) return Ok(result.Message);
            return Ok(result.ResultObj);
        }
        [HttpPost("paging")]
        public async Task<IActionResult> GetAccountProfilePaging(PagingRequestBase request)
        {
            var result = await _accountService.GetAccountProfilePagingAsync(request);
            if (!result.IsSuccess) return Ok(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
