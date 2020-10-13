﻿using System.Threading.Tasks;
using eQACoLTD.Application.Others;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OthersController : ControllerBase
    {
        private readonly IOtherService _otherService;
        public OthersController(IOtherService otherService)
        {
            _otherService = otherService;
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            var result = await _otherService.GetBrandsAsync();
            return Ok(result);
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _otherService.GetCategoriesAsync();
            return Ok(result);
        }
        [HttpGet("customer-types")]
        public async Task<IActionResult> GetCustomerTypes()
        {
            var result = await _otherService.GetCustomertypesAsync();
            return Ok(result);
        }
        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _otherService.GetEmployeesAsync();
            return Ok(result);
        }
    }
}