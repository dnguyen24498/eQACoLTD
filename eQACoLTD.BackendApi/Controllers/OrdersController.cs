using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using eQACoLTD.Application.Order;
using eQACoLTD.ViewModel.Order.Handlers;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOrders(int pageIndex = 1, int pageSize = 15)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _orderService.GetOrdersPagingAsync(employeeId,pageIndex, pageSize);
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOrder(OrderForCreationDto creationDto)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _orderService.CreateOrderAsync(creationDto,employeeId);
            if (result.Code != HttpStatusCode.OK) return StatusCode(500, result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("waiting")]
        public async Task<IActionResult> GetWaitingOrder(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _orderService.GetWaitingOrderAsync(pageIndex, pageSize);
            if (result.Code != HttpStatusCode.OK) return StatusCode(500);
            return Ok(result.ResultObj);
        }

        [HttpPost("waiting/{orderId}/accept")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AcceptWaitingOrder(string orderId,AcceptOrderDto orderDto)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _orderService.AcceptWaitingOrderAsync(employeeId, orderId, orderDto);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            if (result.Code == HttpStatusCode.BadRequest) return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
