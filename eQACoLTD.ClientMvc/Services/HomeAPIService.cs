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

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetBestSellProductsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products/best-sell").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<ListProductHomeResponse>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<ListProductHomeResponse>>>
                    ("Có lỗi khi lấy sản phẩm");
        }

        public async Task<ApiResult<List<CategoryAndBrandsResponse>>> GetCategoryHomeAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/categories/home").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<CategoryAndBrandsResponse>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<CategoryAndBrandsResponse>>>
                    ("Có lỗi khi lấy danh mục");
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetFeaturedProductsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/products/featured").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<ListProductHomeResponse>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<ListProductHomeResponse>>>
                    ("Có lỗi khi lấy sản phẩm");
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetNewArrivedProductsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/products/new-arrived").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<ListProductHomeResponse>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<ListProductHomeResponse>>>
                    ("Có lỗi khi lấy sản phẩm");
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetProductsTopRatedAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/products/top-rated").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<ListProductHomeResponse>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<ListProductHomeResponse>>>
                    ("Có lỗi khi lấy sản phẩm");
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetProductsTopViewdAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/products/top-viewed").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<ListProductHomeResponse>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<ListProductHomeResponse>>>
                    ("Có lỗi khi lấy sản phẩm");
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetRandomProductAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products/suggestion").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<ListProductHomeResponse>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<ListProductHomeResponse>>>
                    ("Có lỗi khi lấy sản phẩm");
        }
    }
}
