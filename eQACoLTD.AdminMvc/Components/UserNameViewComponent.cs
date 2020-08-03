using eQACoLTD.AdminMvc.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Components
{
    public class UserNameViewComponent:ViewComponent
    {
        private readonly IAccountsApiClient _userApiClient;
        public UserNameViewComponent(IAccountsApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userProfile = await _userApiClient.GetAccountProfileAsync();
            var userName = "Unknown";
            if(userProfile!=null) userName = userProfile.ResultObj.UserName;
            return View("Default",userName);
        }
    }
}
