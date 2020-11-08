using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using eQACoLTD.Application.System.Account;
using eQACoLTD.BackendApi.Extensions;
using eQACoLTD.Utilities.Extensions;
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
        private readonly ILoggerManager _loggerManager;

        public AccountsController(IAccountService accountService,ILoggerManager loggerManager)
        {
            _accountService = accountService;
            _loggerManager = loggerManager;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> GetAccountsPaging(int pageIndex=1,int pageSize=15)
        {
            var result = await _accountService.GetAccountsPagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            var result = await _accountService.GetAccountAsync(id);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("{userId}/roles")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> AddRole(Guid userId, [FromBody]Guid roleId)
        {
            var result = await _accountService.AddRoleAsync(userId, roleId);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("{userId}/roles/{roleId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> RemoveRole(Guid userId, Guid roleId)
        {
            var result = await _accountService.RemoveRoleAsync(userId, roleId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{userId}/roles/not-in")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> NotInRoles(Guid userId)
        {
            var result = await _accountService.NotInRolesAsync(userId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("carts")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddProductToCart([FromBody]string productId)
        {
            var customerId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.AddProductToCart(customerId, productId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("carts")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCart()
        {
            var customerId=User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.GetCart(customerId);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("carts/{productId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteProductInCart(string productId)
        {
            var customerId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.DeleteProductFromCart(customerId, productId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("info")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCustomerInfo()
        {
            var customerId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.GetCurrentCustomerInfo(customerId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("carts/create-order")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOrderFromCart()
        {
            var customerId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.CreateOrderFromCartAsync(customerId);
            return StatusCode((int)result.Code, result);
        }
    }
}
