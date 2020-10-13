using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using eQACoLTD.ViewModel.System.Account.Queries;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Services
{
    public class ProductAPIService:IProductAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<string>> PostProductAsync(ProductForCreationDto forCreationDto)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products");
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<string>(System.Net.HttpStatusCode.OK) { 
                    ResultObj=await response.Content.ReadAsStringAsync()
                };
            }
            return new ApiResult<string>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
        public async Task<ApiResult<PagedResult<ProductsDto>>> GetProductPagingAsync(int pageIndex,int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<PagedResult<ProductsDto>>(System.Net.HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<PagedResult<ProductsDto>>
                    (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<PagedResult<ProductsDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}