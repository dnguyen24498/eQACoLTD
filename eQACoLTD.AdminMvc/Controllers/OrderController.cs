using System.Net;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Queries;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderAPIService _apiService;

        public OrderController(IOrderAPIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index(int page = 1,int size=15)
        {
            var result = await _apiService.GetOrdersPagingAsync(page, size);
            if (result.Code!=System.Net.HttpStatusCode.OK) return View(new PagedResult<OrdersDto>());
            return View(result.ResultObj);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Detail(string orderId)
        {
            var result = await _apiService.GetOrderAsync(orderId);
            if (result.Code != HttpStatusCode.OK) return View(new OrderDto());
            return View(result.ResultObj);
        }
    }
}