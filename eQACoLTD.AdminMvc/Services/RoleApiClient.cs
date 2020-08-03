using eQACoLTD.AdminMvc.Constants;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Role.Handlers;
using eQACoLTD.ViewModel.System.Role.Queries;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Services
{
    public class RoleApiClient : IRoleApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoleApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<ApiResult<string>> CreateRolesAsync(CreateRoleRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PagedResult<RoleResponse>>> GetRolesPagingAsync(int page)
        {
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(_httpContextAccessor.HttpContext.Session.GetString("access_token"));
            apiClient.BaseAddress = new System.Uri(ConstantProperties.BackendAPIEndPoint);
            var response = await apiClient.GetAsync(ConstantProperties.GetRolesEndPoint(page));
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<RoleResponse>>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<RoleResponse>>>(await response.Content.ReadAsStringAsync());
        }
    }
}
