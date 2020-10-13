using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Queries;
using eQACoLTD.ViewModel.Product.Category.Queries;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Services
{
    public class CustomerAPIService:ICustomerAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<PagedResult<CustomersDto>>> GetCustomersPagingAsync(int pageIndex,int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/customers?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<PagedResult<CustomersDto>>(System.Net.HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<PagedResult<CustomersDto>>
                    (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<PagedResult<CustomersDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}