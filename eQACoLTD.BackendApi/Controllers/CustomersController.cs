using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Customer;
using eQACoLTD.ViewModel.Customer.Handlers;
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
        public async Task<IActionResult> GetCustomers(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _customerService.GetCustomersPagingAsync(pageIndex, pageSize);
            if (result.Code == HttpStatusCode.NoContent)
                return NoContent();
            return Ok(result.ResultObj);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer(string customerId)
        {
            var result = await _customerService.GetCustomerAsync(customerId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("{customerId}/histories")]
        public async Task<IActionResult> GetCustomerHistories(string customerId, int pageIndex=1, int pageSize=15)
        {
            var result = await _customerService.GetCustomerHistoriesAsync(customerId, pageIndex, pageSize);
            if (result.Code == HttpStatusCode.NoContent) return NoContent();
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerForCreationDto creationDto)
        {
            var result = await _customerService.CreateCustomerAsync(creationDto);
            if (result.Code == HttpStatusCode.InternalServerError)
                return StatusCode(500, result.Message);
            return Ok(result.ResultObj);
        }
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            var result = await _customerService.DeleteCustomerAsync(customerId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("search/{customerName}")]
        public async Task<IActionResult> SearchCustomer(string customerName)
        {
            var customers = await _customerService.SearchCustomerAsync(customerName);
            return Ok(customers.ResultObj);
        }
    }
}
