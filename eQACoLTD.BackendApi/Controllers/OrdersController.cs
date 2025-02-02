﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailService;
using eQACoLTD.Application.Order;
using eQACoLTD.ViewModel.Order.Handlers;
using eQACoLTD.ViewModel.Order.Queries;
using eQACoLTD.ViewModel.System.Account.Queries;
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
        private readonly IEmailSender _emailSender;
        public OrdersController(IOrderService orderService,IEmailSender emailSender)
        {
            _orderService = orderService;
            _emailSender = emailSender;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman,Cashier,WarehouseManager,CashManager,Technician,Accountant")]
        public async Task<IActionResult> GetOrders(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _orderService.GetOrdersPagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }
        [HttpGet("{orderId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman,Cashier,WarehouseManager,CashManager,Technician,Accountant")]
        public async Task<IActionResult> GetOrder(string orderId)
        {
            var result = await _orderService.GetOrderAsync(orderId);
            return StatusCode((int)result.Code, result);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> CreateOrder(OrderForCreationDto creationDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _orderService.CreateOrderAsync(creationDto,accountId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("waiting")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> GetWaitingOrder(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _orderService.GetWaitingOrderAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("waiting/{orderId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> GetWaitingOrderDetail(string orderId)
        {
            var result = await _orderService.GetWaitingOrderDetailAsync(orderId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("waiting/{orderId}/cancel")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> CancelWaitingOrder(string orderId)
        {
            var result = await _orderService.CancelWaitingOrderAsync(orderId);
            return StatusCode((int)result.Code, result);
        }
        [HttpPost("waiting/{orderId}/accept")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> AcceptWaitingOrder(string orderId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _orderService.AcceptWaitingOrderAsync(accountId, orderId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("{orderId}/shipping")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager")]
        public async Task<IActionResult> CreateShippingOrder(string orderId, ShippingOrderDto shippingOrderDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _orderService.CreateShippingOrder(orderId, shippingOrderDto, accountId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("create-for-unknown-user")]
        public async Task<IActionResult> CreateOrderForUnknownUser(CartDto cartDto)
        {
            var result = await _orderService.CreateOrderForUnknownUser(cartDto);
            return StatusCode((int)result.Code, result);
        }
    }
}
