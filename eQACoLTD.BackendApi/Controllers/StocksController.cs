using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager")]
        public async Task<IActionResult> GetExportsQueue(int pageIndex=1,int pageSize=15)
        {
            var result = await _stockService.GetExportQueuePagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }
        [HttpGet("imports")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager")]
        public async Task<IActionResult> GetImportsQueue(int pageIndex=1,int pageSize = 15)
        {
            var result = await _stockService.GetImportQueuePagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }
        [HttpPost("exports/{orderId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager")]
        public async Task<IActionResult> ExportOrder(string orderId,ExportOrderDto orderDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _stockService.ExportOrderAsync(accountId,orderId, orderDto);
            return StatusCode((int)result.Code, result);
        }
        [HttpPost("imports/{purchaseOrderId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager")]
        public async Task<IActionResult> ImportPurchaseOrder(string purchaseOrderId,ImportPurchaseOrderDto orderDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _stockService.ImportPurchaseOrderAsync(accountId, purchaseOrderId, orderDto);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("exports/{orderId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager")]
        public async Task<IActionResult> IsExportOrder(string orderId)
        {
            var result = await _stockService.OrderIsExport(orderId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("exports/{orderId}/export-histories")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager,Accountant")]
        public async Task<IActionResult> GetOrderExportHistories(string orderId)
        {
            var result = await _stockService.GetExportOrderHistory(orderId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("products")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager,Accountant,Salesman")]
        public async Task<IActionResult> GetProductsInStock(int pageIndex=1, int pageSize=15)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _stockService.GetProductsInStockPagingAsync(pageIndex, pageSize, accountId);
            return StatusCode((int)result.Code, result);
        }
    }
}
    