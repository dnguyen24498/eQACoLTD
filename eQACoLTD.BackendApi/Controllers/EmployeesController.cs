using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.Application.System.Employee;
using eQACoLTD.ViewModel.System.Employee.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeePaging(int pageIndex = 1)
        {
            var result = await _employeeService.GetEmployeesPagingAsync(pageIndex);
            return Ok(result);
        }
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee(string employeeId)
        {
            var result = await _employeeService.GetEmployeeAsync(employeeId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody]PostEmployeeRequest request)
        {
            var result = await _employeeService.PostEmployeeAsync(request);
            return Ok(result);
        }
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(string employeeId)
        {
            await _employeeService.DeleteEmployeeAsync(employeeId);
            return Ok();
        }
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> PutEmployee(string employeeId,[FromBody] PutEmployeeRequest request)
        {
            var result = await _employeeService.PutEmployeeAsync(employeeId, request);
            return Ok(result);
        }
    }
}
