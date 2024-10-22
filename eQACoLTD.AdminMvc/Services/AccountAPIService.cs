﻿using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;

namespace eQACoLTD.AdminMvc.Services
{
    public class AccountAPIService : IAccountAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<ApiResult<AccountDto>> GetAccountDetailAsync(Guid userId)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/accounts/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<AccountDto>>(await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<AccountDto>(response.StatusCode,await response.Content.ReadAsStringAsync()); 
        }

        public async Task<ApiResult<EmployeeInfo>> GetCurrentAccountInfo()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/accounts/info");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<EmployeeInfo>>(await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<EmployeeInfo>(response.StatusCode,await response.Content.ReadAsStringAsync()); 
        }


        public async Task<ApiResult<PagedResult<AccountsDto>>> GetAccountsPagingAsync(int pageIndex,int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/accounts?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<PagedResult<AccountsDto>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<PagedResult<AccountsDto>>(response.StatusCode,await response.Content.ReadAsStringAsync());
        }
    }
}
