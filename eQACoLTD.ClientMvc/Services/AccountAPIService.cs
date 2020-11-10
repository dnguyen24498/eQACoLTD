using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eQACoLTD.ClientMvc.Services
{
    public class AccountAPIService:IAccountAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<int>> AddProductToCart(string productId)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var json = JsonConvert.SerializeObject(productId);
            var httpContent=new StringContent(json,Encoding.UTF8,"application/json");
            var response = await httpClient.PostAsync("api/accounts/carts",httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<int>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<int>(response.StatusCode);
        }

        public async Task<ApiResult<CartDto>> GetCart()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/accounts/carts");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<CartDto>>
                        (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<CartDto>(response.StatusCode);
        }

        public async Task<ApiResult<string>> CreateOrderFromCartAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.PostAsync("api/accounts/carts/create-order",new StringContent(""));
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<string>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<string>(response.StatusCode);
        }

        public async Task<ApiResult<CustomerInfo>> GetCurrentAccountInfo()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/accounts/info");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<CustomerInfo>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<CustomerInfo>(response.StatusCode);
        }
    }
}