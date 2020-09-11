using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.Application.Customer;
using eQACoLTD.ViewModel.Customer.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetCustomerPaging(int pageIndex = 1)
        {
            var result = await _customerService.GetCustomersPagingAsync(pageIndex);
            return Ok(result);
        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer(string customerId)
        {
            var result = await _customerService.GetCustomerAsync(customerId);
            return Ok(result);
        }
        [HttpGet("{customerId}/histories")]
        public async Task<IActionResult> GetCustomerHistory(string customerId,int pageIndex)
        {
            var result = await _customerService.GetCustomerHistoryPagingAsync(customerId,pageIndex);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PostCustomer(CustomerRequest request)
        {
            var result = await _customerService.PostCustomerAsync(request);
            return Ok(result);
        }
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            await _customerService.DeleteCustomerAsync(customerId);
            return Ok();
        }
        [HttpPut("{customerId}")]
        public async Task<IActionResult> PutCustomer(string customerId,CustomerRequest request)
        {
            var result = await _customerService.PutCustomerAsync(customerId, request);
            return Ok(result);
        }
    }
}
