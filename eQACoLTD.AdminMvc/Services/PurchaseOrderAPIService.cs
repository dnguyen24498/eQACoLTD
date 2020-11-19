using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Queries;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Services
{
    public class PurchaseOrderAPIService:IPurchaseOrderAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PurchaseOrderAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<PagedResult<PurchaseOrdersDto>>> GetPurchaseOrderPagingAsync(int pageIndex, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/purchaseorders?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<PagedResult<PurchaseOrdersDto>>>
                    (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<PagedResult<PurchaseOrdersDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}