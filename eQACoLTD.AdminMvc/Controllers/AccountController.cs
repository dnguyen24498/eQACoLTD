using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Handlers;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Controllers
{
    [CustomAuthorize(Permissions = "Administrator")]
    public class AccountController : Controller
    {
        private readonly IAccountAPIService _accountAPIService;
        public AccountController(IAccountAPIService accountAPIService)
        {
            _accountAPIService = accountAPIService;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            var result = await _accountAPIService.GetAccountsPagingAsync(page);
            if (!result.IsSuccess) View("Index", new PagedResult<AccountResponse>());
            return View(result.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id) 
        {
            var result = await _accountAPIService.GetAccountDetailAsync(id);
            if (!result.IsSuccess) return View("404");
            return View(result.ResultObj);
        }
    }
}
