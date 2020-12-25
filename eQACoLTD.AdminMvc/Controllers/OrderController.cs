using System.Net;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderAPIService _apiService;
        private readonly IConfiguration _configuration;

        public OrderController(IOrderAPIService apiService,IConfiguration configuration)
        {
            _apiService = apiService;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(int page = 1,int size=15)
        {
            var result = await _apiService.GetOrdersPagingAsync(page, size);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
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
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if (result.Code != HttpStatusCode.OK) return View(new OrderDto());
            return View(result.ResultObj);
        }

        public async Task<IActionResult> Waiting(int page = 1,int size=15)
        {
            var result = await _apiService.GetWaitingOrderAsync(page, size);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if(result.Code!=HttpStatusCode.OK) return View(new PagedResult<OrdersDto>());
            return View(result.ResultObj);
        }

        public async Task<IActionResult> WaitingOrderDetail(string orderId)
        {
            var result = await _apiService.GetWaitingOrderDetailAsync(orderId);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if(result.Code!=HttpStatusCode.OK) return View(new WaitingOrderDto());
            return View(result.ResultObj);
        }

        public async Task<IActionResult> AcceptWaitingOrder(string orderId)
        {
            await _apiService.AcceptWaitingOrderAsync(orderId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelWaitingOrder(string orderId)
        {
            await _apiService.CancelWaitingOrderAsync(orderId);
            return RedirectToAction("Waiting", new {page = 1, size = 15});
        }
    }
}