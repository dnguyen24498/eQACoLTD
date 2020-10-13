using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Order;
using eQACoLTD.ViewModel.Order.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders(string brachId,int pageIndex = 1, int pageSize = 15)
        {
            var result = await _orderService.GetOrdersPagingAsync(brachId,pageIndex, pageSize);
            if (result.Code != HttpStatusCode.OK) return StatusCode(500);
            return Ok(result.ResultObj);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(string orderId)
        {
            var result = await _orderService.GetOrderAsync(orderId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderForCreationDto creationDto)
        {
            var result = await _orderService.CreateOrderAsync(creationDto);
            if (result.Code != HttpStatusCode.OK) return StatusCode(500, result.Message);
            return Ok(result.ResultObj);
        }
        [HttpPost("{orderId}/export")]
        public async Task<IActionResult> ExportProductsInOrder(string orderId,OrderGoodsDeliveryNoteForCreationDto creationDto)
        {
            var result = await _orderService.ExportStockOrderAsync(orderId, creationDto);
            if (result.Code == HttpStatusCode.InternalServerError) return StatusCode(500, result.Message);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
        [HttpGet("{branchId}/export-queue")]
        public async Task<IActionResult> ExportsQueue(string branchId,int pageIndex=1,int pageSize = 15)
        {
            var result = await _orderService.GetExportOrdersPagingAsync(branchId,pageIndex, pageSize);
            return Ok(result.ResultObj);
        }
        [HttpPost("receipt-vouchers")]
        public async Task<IActionResult> AddReceiptVouchers(string orderId,OrderReceiptVoucherForCreationDto creationDto)
        {
            var result = await _orderService.AddOrderReceiptVoucherAsync(orderId, creationDto);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            if (result.Code == HttpStatusCode.BadRequest) return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
