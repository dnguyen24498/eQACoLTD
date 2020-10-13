using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<IActionResult> GetSuppliers(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _supplierService.GetSuppliersPagingAsync(pageIndex, pageSize);
            if (result.Code == HttpStatusCode.NoContent)
                return NoContent();
            return Ok(result.ResultObj);
        }

        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSupplier(string supplierId)
        {
            var result = await _supplierService.GetSupplierAsync(supplierId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierForCreationDto creationDto)
        {
            var result = await _supplierService.CreateSupplierAsync(creationDto);
            if (result.Code == HttpStatusCode.InternalServerError)
                return StatusCode(500, result.Message);
            return Ok(result.ResultObj);
        }

        [HttpDelete("{supplierId}")]
        public async Task<IActionResult> DeleteSupplier(string supplierId)
        {
            var result = await _supplierService.DeleteSupplierAsync(supplierId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("{supplierId}/import-histories")]
        public async Task<IActionResult> ImportHistories(string supplierId, int pageIndex = 1, int pageSize = 15)
        {
            var result = await _supplierService.GetSupplierImportHistoriesPagingAsync(supplierId, pageIndex, pageSize);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
