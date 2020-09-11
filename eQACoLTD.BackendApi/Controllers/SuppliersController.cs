using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.Application.Product.Supplier;
using eQACoLTD.ViewModel.Product.Supplier.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSupplierPaging(int pageIndex=1)
        {
            var result = await _supplierService.GetSupplierPagingAsync(pageIndex);
            return Ok(result);
        }

        [HttpGet("{supplierId}")]   
        public async Task<IActionResult> GetSupplier(string supplierId)
        {
            var result = await _supplierService.GetSupplierAsync(supplierId);
            return Ok(result);
        }
        [HttpGet("{supplierId}/histories")]
        public async Task<IActionResult> GetSupplierHistory(string supplierId,int pageIndex=1)
        {
            var result = await _supplierService.GetSupplierGoodsReceiptHistoryPagingAsync(supplierId,pageIndex);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PostSupplier([FromBody] SupplierRequest request)
        {
            var result = await _supplierService.PostSupplierAsync(request);
            return Ok(result);
        }
        [HttpDelete("{supplierId}")]
        public async Task<IActionResult> DeleteSupplier(string supplierId)
        {
            await _supplierService.DeleteSupplierAsync(supplierId);
            return Ok();
        }
    }
}
