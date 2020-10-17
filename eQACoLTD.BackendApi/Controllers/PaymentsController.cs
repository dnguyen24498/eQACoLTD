using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOrderPayment(string orderId,OrderPaymenForCreationDto creationDto)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _paymentService.OrderReceiveAsync(employeeId,orderId,creationDto);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            if (result.Code == HttpStatusCode.BadRequest) return BadRequest(result.Message);
            if (result.Code == HttpStatusCode.Forbidden) return Forbid(result.Message);
            return Ok(result.ResultObj);
        }
        [HttpPost("purchase-orders/{purchaseOrderId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreatePurchaseOrderPayment(string purchaseOrderId, PurchaseOrderPaymentForCreationDto creationDto)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _paymentService.PurchaseOrderPaymentAsync(employeeId, purchaseOrderId, creationDto);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            if (result.Code == HttpStatusCode.BadRequest) return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }
        [HttpGet("orders/{orderId}/is-paid")]
        public async Task<IActionResult> CheckPaidOrder(string orderId)
        {
            var result = await _paymentService.IsPaidOrder(orderId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("orders/{orderId}")]
        public async Task<IActionResult> GetPaymentsOrder(string orderId)
        {
            var result = await _paymentService.GetOrderPaymentHistory(orderId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
