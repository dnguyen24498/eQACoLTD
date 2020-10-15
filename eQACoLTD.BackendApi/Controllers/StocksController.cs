using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Product.Stock;
using eQACoLTD.ViewModel.Product.Stock.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }
        [HttpGet("exports")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetExportsQueue(int pageIndex=1,int pageSize=15)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _stockService.GetExportQueuePagingAsync(employeeId, pageIndex, pageSize);
            return Ok(result.ResultObj);
        }
        [HttpGet("imports")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetImportsQueue(int pageIndex=1,int pageSize = 15)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _stockService.GetImportQueuePagingAsync(employeeId, pageIndex, pageSize);
            return Ok(result.ResultObj);
        }
        [HttpPost("exports/{orderId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ExportOrder(string orderId,ExportOrderDto orderDto)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _stockService.ExportOrderAsync(employeeId,orderId, orderDto);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            if (result.Code == HttpStatusCode.InternalServerError) return StatusCode(500, result.Message);
            if (result.Code == HttpStatusCode.Forbidden) return Forbid(result.Message);
            return Ok(result.ResultObj);
        }
        [HttpPost("imports/{purchaseOrderId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ImportPurchaseOrder(string purchaseOrderId,ImportPurchaseOrderDto orderDto)
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _stockService.ImportPurchaseOrderAsync(employeeId, purchaseOrderId, orderDto);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            if (result.Code == HttpStatusCode.BadRequest) return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("exports/{orderId}")]
        public async Task<IActionResult> IsExportOrder(string orderId)
        {
            var result = await _stockService.OrderIsExport(orderId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
