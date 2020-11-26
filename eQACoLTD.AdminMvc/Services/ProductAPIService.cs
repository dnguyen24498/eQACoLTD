using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using eQACoLTD.ViewModel.Product.Stock.Queries;
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
                return JsonConvert.DeserializeObject<ApiResult<string>>(await response.Content.ReadAsStringAsync()
                );
            }
            return new ApiResult<string>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<PagedResult<ProductInStock>>> GetProductsInStockPagingAsync(int pageIndex, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/stocks/products");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<PagedResult<ProductInStock>>>(await response.Content.ReadAsStringAsync()
                );
            }
            return new ApiResult<PagedResult<ProductInStock>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<PagedResult<PromotionsDto>>> GetPromotionsPagingAsync(int pageIndex, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products/promotions?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<PagedResult<PromotionsDto>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<PagedResult<PromotionsDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<PagedResult<GoodsDeliveryNotesDto>>> GetGoodsDeliveryNotePagingAsync(int pageIndex, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/stocks/goods-delivery-notes?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<PagedResult<GoodsDeliveryNotesDto>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<PagedResult<GoodsDeliveryNotesDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<GoodsDeliveryNoteDto>> GetGoodsDeliveryNote(string goodsDeliveryNoteId)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Stocks/goods-delivery-notes/{goodsDeliveryNoteId}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<GoodsDeliveryNoteDto>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<GoodsDeliveryNoteDto>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<PagedResult<ProductsDto>>> GetProductPagingAsync(int pageIndex,int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/products?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<PagedResult<ProductsDto>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<PagedResult<ProductsDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}