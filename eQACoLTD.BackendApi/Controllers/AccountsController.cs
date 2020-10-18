using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.System.Account;
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
        public async Task<IActionResult> GetAccountsPaging(int pageIndex=1,int pageSize=15)
        {
            var result = await _accountService.GetAccountsPagingAsync(pageIndex, pageSize);
            if(result.Code==HttpStatusCode.OK) return Ok(result.ResultObj);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            var result = await _accountService.GetAccountAsync(id);
            if (result.Code == HttpStatusCode.NotFound)
                return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("{userId}/roles")]
        public async Task<IActionResult> AddRole(Guid userId, [FromBody]Guid roleId)
        {
            var result = await _accountService.AddRoleAsync(userId, roleId);
            if (result.Code == HttpStatusCode.NotFound||result.Code==HttpStatusCode.NotModified)
                return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpDelete("{userId}/roles/{roleId}")]
        public async Task<IActionResult> RemoveRole(Guid userId, Guid roleId)
        {
            var result = await _accountService.RemoveRoleAsync(userId, roleId);
            if (result.Code == HttpStatusCode.NotFound||result.Code==HttpStatusCode.NotModified)
                return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("{userId}/roles/not-in")]
        public async Task<IActionResult> NotInRoles(Guid userId)
        {
            var result = await _accountService.NotInRolesAsync(userId);
            if (result.Code == HttpStatusCode.NotFound)
                return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("carts")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddProductToCart([FromBody]string productId)
        {
            var customerId=User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _accountService.AddProductToCart(customerId, productId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("carts")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCart()
        {
            var customerId=User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _accountService.GetCart(customerId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpDelete("carts/{productId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteProductInCart(string productId)
        {
            var customerId=User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _accountService.DeleteProductFromCart(customerId, productId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("info")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCustomerInfo()
        {
            var customerId=User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _accountService.GetCurrentCustomerInfo(customerId);
            if(result.Code==HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
