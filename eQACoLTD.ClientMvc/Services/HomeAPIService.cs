using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace eQACoLTD.ClientMvc.Services
{
    public class HomeAPIService : IHomeAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<List<ProductCardDto>>> GetBestSellProductsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products/best-selling").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<List<ProductCardDto>>(HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<List<ProductCardDto>>
                        (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<List<ProductCardDto>>(HttpStatusCode.NotFound);
        }

        public async Task<ApiResult<List<CategoriesDto>>> GetCategoryHomeAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/categories/all").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<List<CategoriesDto>>(HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<List<CategoriesDto>>
                        (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<List<CategoriesDto>>(HttpStatusCode.NotFound);
        }

        public async Task<ApiResult<List<ProductCardDto>>> GetFeaturedProductsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/products/best-selling").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<List<ProductCardDto>>(HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<List<ProductCardDto>>
                        (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<List<ProductCardDto>>(HttpStatusCode.NotFound);
        }

        public async Task<ApiResult<PagedResult<ProductCardDto>>> GetProductsByCategoryPagingAsync(string categoryId, int pageIndex, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.
                GetAsync($"api/categories/{categoryId}/products?pageIndex={pageIndex}&pageSize={pageSize}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<PagedResult<ProductCardDto>>(HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<PagedResult<ProductCardDto>>
                        (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<PagedResult<ProductCardDto>>(HttpStatusCode.NotFound);
        }

        public async Task<ApiResult<List<ProductCardDto>>> GetNewArrivedProductsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/products/new-arrived").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<List<ProductCardDto>>(HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<List<ProductCardDto>>
                        (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<List<ProductCardDto>>(HttpStatusCode.NotFound);
        }

        public async Task<ApiResult<List<ProductCardDto>>> GetProductsTopRatedAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/products/top-rate").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<List<ProductCardDto>>(HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<List<ProductCardDto>>
                        (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<List<ProductCardDto>>(HttpStatusCode.NotFound);
        }

        public async Task<ApiResult<List<ProductCardDto>>> GetProductsTopViewAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/products/top-view").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<List<ProductCardDto>>(HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<List<ProductCardDto>>
                        (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<List<ProductCardDto>>(HttpStatusCode.NotFound);
        }

        public async Task<ApiResult<List<ProductCardDto>>> GetRandomProductAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products/random").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<List<ProductCardDto>>(HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<List<ProductCardDto>>
                        (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<List<ProductCardDto>>(HttpStatusCode.NotFound);
        }
    }
}
