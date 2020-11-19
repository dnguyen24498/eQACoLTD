using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using eQACoLTD.Application.Product.Supplier;
using eQACoLTD.ViewModel.Product.Supplier.Handlers;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Accountant,BusinessStaff")]
        public async Task<IActionResult> GetSuppliers(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _supplierService.GetSuppliersPagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{supplierId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Accountant,BusinessStaff")]
        public async Task<IActionResult> GetSupplier(string supplierId)
        {
            var result = await _supplierService.GetSupplierAsync(supplierId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> CreateSupplier(SupplierForCreationDto creationDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _supplierService.CreateSupplierAsync(creationDto,accountId);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("{supplierId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> DeleteSupplier(string supplierId)
        {
            var result = await _supplierService.DeleteSupplierAsync(supplierId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{supplierId}/import-histories")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Accountant,BusinessStaff")]
        public async Task<IActionResult> ImportHistories(string supplierId, int pageIndex = 1, int pageSize = 15)
        {
            var result = await _supplierService.GetSupplierImportHistoriesPagingAsync(supplierId, pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("search")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> SearchSupplier(string searchValue)
        {
            var result = await _supplierService.SearchSupplier(searchValue);
            return StatusCode((int)result.Code, result);
        }
    }
}
