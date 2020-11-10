using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountAPIService _accountAPIService;
        public AccountController(IAccountAPIService accountAPIService)
        {
            _accountAPIService = accountAPIService;
        }
        [CustomAuthorize(Permissions = "Administrator")]
        public async Task<IActionResult> Index(int page=1,int size=15)
        {
            var result = await _accountAPIService.GetAccountsPagingAsync(page,size);
            if (result.Code!=HttpStatusCode.OK) View("Index", new PagedResult<AccountsDto>());
            return View(result.ResultObj);
        }

        [HttpGet]
        [CustomAuthorize(Permissions = "Administrator")]
        public async Task<IActionResult> Detail(Guid id) 
        {
            var result = await _accountAPIService.GetAccountDetailAsync(id);
            if (result.Code!=HttpStatusCode.OK) return View();
            return View(result.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            
            return View();
        }
    }
}
