using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Services
{
    public class SupplierAPIService:ISupplierAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SupplierAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<PagedResult<SuppliersDto>>> GetSupplierPagingAsync(int pageIndex,int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/suppliers?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult<PagedResult<SuppliersDto>>(System.Net.HttpStatusCode.OK)
                {
                    ResultObj = JsonConvert.DeserializeObject<PagedResult<SuppliersDto>>
                    (await response.Content.ReadAsStringAsync())
                };
            }
            return new ApiResult<PagedResult<SuppliersDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}