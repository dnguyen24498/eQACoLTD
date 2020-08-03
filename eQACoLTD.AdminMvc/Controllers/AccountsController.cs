using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Constants;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.User.Queries;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccountApiClient _accountApiClient;

        public AccountsController(IHttpClientFactory httpClientFactory,
            IAccountApiClient accountApiClient)
        {
            _httpClientFactory = httpClientFactory;
            _accountApiClient = accountApiClient;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            var result = await _accountApiClient.GetAccountProfilePagingAsync(page);
            if (!result.IsSuccess) return Ok(result.Message);
            return View(result.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Roles(string userName)
        {
            var result = await _accountApiClient.GetAccountRolesAsync(userName);
            if (!result.IsSuccess) return Ok(result.Message);
            return View(result.ResultObj);
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateRoles(AccountRolesResponse request)
        {
            var updateRequest = new UpdateAccountRoleRequest()
            {
                AddRoleNames = request.NotInRoles,
                DeleteRoleNames = request.InRoles
            };
            var result = await _accountApiClient.UpdateAccountRolesAsync(request.UserName, updateRequest);
            if(!result.IsSuccess) return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index));
        }

    }
}
