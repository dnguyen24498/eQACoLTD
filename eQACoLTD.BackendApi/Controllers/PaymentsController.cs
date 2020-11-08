using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using eQACoLTD.Application.Product.Payment;
using eQACoLTD.ViewModel.Product.Payment.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("orders/{orderId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Cashier,CashManager")]
        public async Task<IActionResult> CreateOrderPayment(string orderId,OrderPaymenForCreationDto creationDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _paymentService.OrderReceiveAsync(accountId,orderId,creationDto);
            return StatusCode((int)result.Code, result);
        }
        [HttpPost("purchase-orders/{purchaseOrderId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,CashManager")]
        public async Task<IActionResult> CreatePurchaseOrderPayment(string purchaseOrderId, PurchaseOrderPaymentForCreationDto creationDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _paymentService.PurchaseOrderPaymentAsync(accountId, purchaseOrderId, creationDto);
            return StatusCode((int)result.Code, result);
        }
        [HttpGet("orders/{orderId}/is-paid")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Cashier,CashManager")]
        public async Task<IActionResult> CheckPaidOrder(string orderId)
        {
            var result = await _paymentService.IsPaidOrder(orderId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("orders/{orderId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Cashier,CashManager,Accountant")]
        public async Task<IActionResult> GetPaymentsOrder(string orderId)
        {
            var result = await _paymentService.GetOrderPaymentHistory(orderId);
            return StatusCode((int)result.Code, result);
        }
    }
}
