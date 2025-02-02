﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using eQACoLTD.Application.System.Account;
using eQACoLTD.BackendApi.Extensions;
using eQACoLTD.Utilities.Extensions;
using eQACoLTD.ViewModel.System.Account.Handlers;
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
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.AddRoleAsync(userId, roleId,accountId);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("{userId}/roles/{roleId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> RemoveRole(Guid userId, Guid roleId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.RemoveRoleAsync(userId, roleId,accountId);
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
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.AddProductToCart(accountId, productId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("carts")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCart()
        {
            var accountId=User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.GetCart(accountId);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("carts/{productId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteProductInCart(string productId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.DeleteProductFromCart(accountId, productId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("info")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAccountInfo()
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.GetCurrentAccountInfo(accountId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("carts/create-order")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOrderFromCart()
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.CreateOrderFromCartAsync(accountId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPut("info")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateAccountInfo([FromBody] AccountForUpdateDto updateDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.UpdateAccountInfo(updateDto, accountId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("orders")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAccountOrders(int pageIndex = 1, int pageSize = 15)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.GetAccountOrders(pageIndex, pageSize,accountId);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("orders/{orderId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _accountService.CancelOrder(orderId, accountId);
            return StatusCode((int)result.Code, result);
        }
    }
}
