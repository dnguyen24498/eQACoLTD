using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Product.PurchaseOrder;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetPurchaseOrders(int pageIndex=1,int pageSize = 15)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _purchaseOrderService.GetPurchaseOrderPagingAsync(employeeId, pageIndex, pageSize);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
        [HttpGet("{purchaseOrderId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetPurchaseOrder(string purchaseOrderId)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _purchaseOrderService.GetPurchaseOrderAsync(purchaseOrderId, employeeId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderForCreationDto creationDto)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _purchaseOrderService.CreatePurchaseOrderAsync(employeeId, creationDto);
            if (result.Code == HttpStatusCode.InternalServerError) return StatusCode(500, result.Message);
            return Ok(result.ResultObj);
        }
    }
}
