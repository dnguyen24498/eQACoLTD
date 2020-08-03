using eQACoLTD.AdminMvc.Constants;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.User.Queries;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Services
{
    public class AccountApiClient : IAccountApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountApiClient(IHttpClientFactory httpClientFactory,IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<PagedResult<UserProfileResponse>>> GetAccountProfilePagingAsync(int page)
        {
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(_httpContextAccessor.HttpContext.Session.GetString("access_token"));
            apiClient.BaseAddress = new System.Uri(ConstantProperties.BackendAPIEndPoint);
            var response = await apiClient.GetAsync(ConstantProperties.GetAccountPagingEndPoint(page));
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserProfileResponse>>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<UserProfileResponse>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<AccountRolesResponse>> GetAccountRolesAsync(string userName)
        {
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(_httpContextAccessor.HttpContext.Session.GetString("access_token"));
            apiClient.BaseAddress = new System.Uri(ConstantProperties.BackendAPIEndPoint);
            var response = await apiClient.GetAsync(ConstantProperties.GetAccountRolesEndPoint(userName));
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<AccountRolesResponse>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<AccountRolesResponse>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<string>> UpdateAccountRolesAsync(string userName, UpdateAccountRoleRequest request)
        {
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(_httpContextAccessor.HttpContext.Session.GetString("access_token"));
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            apiClient.BaseAddress = new System.Uri(ConstantProperties.BackendAPIEndPoint);
            var response = await apiClient.PutAsync(ConstantProperties.PutAccountRolesEndPoint(userName), httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
        }
    }
}
