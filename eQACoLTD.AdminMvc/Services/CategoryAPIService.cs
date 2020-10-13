using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Services
{
    public class CategoryAPIService:ICategoryAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<PagedResult<CategoriesDto>>> GetCategoryPagingAsync(int pageIndex,int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/categories?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<PagedResult<CategoriesDto>>(System.Net.HttpStatusCode.OK) { 
                    ResultObj= JsonConvert.DeserializeObject<PagedResult<CategoriesDto>>
                    (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<PagedResult<CategoriesDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}