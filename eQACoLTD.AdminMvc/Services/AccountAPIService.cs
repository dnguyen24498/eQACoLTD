using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Services
{
    public class AccountAPIService : IAccountAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<ApiResult<AccountDetailResponse>> GetAccountDetailAsync(Guid userId)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/accounts/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<AccountDetailResponse>>
                    (await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<AccountDetailResponse>>
                    ("Không tìm thấy tài khoản");
        }

        public async Task<ApiResult<PagedResult<AccountResponse>>> GetAccountsPagingAsync(int pageIndex)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/accounts?pageIndex={pageIndex}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<AccountResponse>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<AccountResponse>>>
                    ("Có lỗi khi lấy danh sách tài khoản");
        }
    }
}
