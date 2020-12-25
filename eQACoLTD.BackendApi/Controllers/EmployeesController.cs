using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.System.Employee;
using eQACoLTD.ViewModel.System.Employee.Handlers;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> GetEmployeesPaging(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _employeeService.GetEmployeesPagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{employeeId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> GetEmployee(string employeeId)
        {
            var result = await _employeeService.GetEmployeeAsync(employeeId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> CreateEmployee(EmployeeForCreationDto creationDto)
        {
            var result = await _employeeService.CreateEmployeeAsync(creationDto);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("{employeeId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator")]
        public async Task<IActionResult> DeleteEmployee(string employeeId)
        {
            var result = await _employeeService.DeleteEmployeeAsync(employeeId);
            return StatusCode((int)result.Code, result);
        }
    }
}
