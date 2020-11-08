using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Queries;
using eQACoLTD.ViewModel.Product.Category.Queries;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Services
{
    public class OrderAPIService:IOrderAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<PagedResult<OrdersDto>>> GetOrdersPagingAsync(int pageIndex, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/orders?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<PagedResult<OrdersDto>>>
                        (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<PagedResult<OrdersDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<OrderDto>> GetOrderAsync(string orderId)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/orders/"+orderId);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<OrderDto>>
                        (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<OrderDto>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<PagedResult<OrdersDto>>> GetWaitingOrderAsync(int pageIndex, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Orders/waiting?pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<PagedResult<OrdersDto>>>
                        (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<PagedResult<OrdersDto>>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<WaitingOrderDto>> GetWaitingOrderDetailAsync(string orderId)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Orders/waiting/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<WaitingOrderDto>>
                        (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<WaitingOrderDto>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<string>> AcceptWaitingOrderAsync(string waitingOrderId)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var json = JsonConvert.SerializeObject("");
            var httpContent=new StringContent(json,Encoding.UTF8,"application/json");
            var response = await httpClient.PostAsync($"api/Orders/waiting/{waitingOrderId}/accept",httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<string>>
                        (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<string>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<string>> CancelWaitingOrderAsync(string waitingOrderId)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var json = JsonConvert.SerializeObject("");
            var httpContent=new StringContent(json,Encoding.UTF8,"application/json");
            var response = await httpClient.PostAsync($"api/Orders/waiting/{waitingOrderId}/cancel",httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<string>>
                        (await response.Content.ReadAsStringAsync());
            }
            return new ApiResult<string>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}