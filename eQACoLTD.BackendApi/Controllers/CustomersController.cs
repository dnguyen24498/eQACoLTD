using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using eQACoLTD.Application.Customer;
using eQACoLTD.ViewModel.Customer.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> GetCustomers(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _customerService.GetCustomersPagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{customerId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> GetCustomer(string customerId)
        {
            var result = await _customerService.GetCustomerAsync(customerId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{customerId}/histories")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> GetCustomerHistories(string customerId, int pageIndex=1, int pageSize=15)
        {
            var result = await _customerService.GetCustomerHistoriesAsync(customerId, pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> CreateCustomer(CustomerForCreationDto creationDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _customerService.CreateCustomerAsync(creationDto,accountId);
            return StatusCode((int)result.Code, result);
        }
        [HttpDelete("{customerId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman")]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            var result = await _customerService.DeleteCustomerAsync(customerId);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("search/{customerName}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Salesman,Cashier,WarehouseManager,CashManager,BusinessStaff,Technician,Accountant,Manager")]
        public async Task<IActionResult> SearchCustomer(string customerName)
        {
            var result = await _customerService.SearchCustomerAsync(customerName);
            return StatusCode((int)result.Code, result);
        }
        
    }
}
