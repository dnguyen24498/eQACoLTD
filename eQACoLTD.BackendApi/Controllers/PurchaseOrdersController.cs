using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager,CashManager,BusinessStaff,Accountant")]
        public async Task<IActionResult> GetPurchaseOrders(int pageIndex=1,int pageSize = 15)
        {
            var result = await _purchaseOrderService.GetPurchaseOrderPagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }
        [HttpGet("{purchaseOrderId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseManager,CashManager,BusinessStaff,Accountant")]
        public async Task<IActionResult> GetPurchaseOrder(string purchaseOrderId)
        {
            var result = await _purchaseOrderService.GetPurchaseOrderAsync(purchaseOrderId);
            return StatusCode((int)result.Code, result);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderForCreationDto creationDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _purchaseOrderService.CreatePurchaseOrderAsync(accountId, creationDto);
            return StatusCode((int)result.Code, result);
        }
    }
}
