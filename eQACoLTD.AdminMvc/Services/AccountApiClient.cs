using eQACoLTD.AdminMvc.Constants;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.User.Queries;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Services
{
    public class AccountsApiClient : IAccountsApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountsApiClient(IHttpClientFactory httpClientFactory,IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<UserProfileResponse>> GetAccountProfileAsync()
        {
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(_httpContextAccessor.HttpContext.Session.GetString("access_token"));
            apiClient.BaseAddress = new System.Uri(ConstantProperties.BackendAPIEndPoint);
            var response = await apiClient.GetAsync(ConstantProperties.GetAccountProfileEndPoint);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<UserProfileResponse>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<UserProfileResponse>>(await response.Content.ReadAsStringAsync());
        }
    }
}
