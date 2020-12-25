using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Newtonsoft.Json;

namespace eQACoLTD.ClientMvc.Services
{
    public class ProductAPIService:IProductAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<ProductDto>> GetProductAsync(string productId)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products/{productId}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<ProductDto>>
                        (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<ProductDto>(HttpStatusCode.NotFound);
        }

        public async Task<ApiResult<PromotionDto>> GetClosetPromotion()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products/closet-promotions");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<PromotionDto>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<PromotionDto>(HttpStatusCode.NotFound);
        }
    }
}