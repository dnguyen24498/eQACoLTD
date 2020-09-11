using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.Application.System.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAccounts(int pageIndex)
        {
            var result = await _accountService.GetAccountsPagingAsync(pageIndex);
            return Ok(result);
        }
        [HttpGet("{userId}")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAccountDetail(Guid userId)
        {
            var result = await _accountService.GetAccountDetailAsync(userId);
            if (!result.IsSuccess) return NotFound();
            return Ok(result);
        }
        [HttpGet("{userId}/not-in-roles")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAccountNotInRoles(Guid userId)
        {
            var result = await _accountService.GetAccountNotInRolesAsync(userId);
            if (!result.IsSuccess) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{userId}/roles")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteAccountRole(Guid userId,[FromBody]Guid roleId)
        {
            await _accountService.DeleteAccountRoleAsync(userId, roleId);
            return Ok();
        }
        [HttpPost("{userId}/roles")]
        [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddAccountRole(Guid userId,[FromBody]Guid roleId)
        {
            var result = await _accountService.AddAccountRoleAsync(userId, roleId);
            if (!result.IsSuccess) return BadRequest();
            return Ok(result);
        }
    }
}
