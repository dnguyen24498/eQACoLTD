using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.User.Queries;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Controllers
{
    [Authorize]
    public class SystemController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SystemController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Accounts(int page=1)
        {
            var acessToken = await HttpContext.GetTokenAsync("access_token");
            var result = await GetResultApi(acessToken,page);
            return View(result);
        }

        private async Task<PagedResult<UserProfileResponse>> GetResultApi(string accessToken,int page)
        {
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(accessToken);
            var json=JsonConvert.SerializeObject(new PagingRequestBase() { 
                PageIndex=page,
                PageSize=2
            });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await apiClient.PostAsync("https://localhost:5001/api/accounts/paging",httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PagedResult<UserProfileResponse>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<PagedResult<UserProfileResponse>>(await response.Content.ReadAsStringAsync());
        }
    }
}
