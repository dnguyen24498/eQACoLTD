using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<IActionResult> GetEmployeesPaging(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _employeeService.GetEmployeeesPagingAsync(pageIndex, pageSize);
            if (result.Code == HttpStatusCode.OK)
                return Ok(result.ResultObj);
            return BadRequest();
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee(string employeeId)
        {
            var result = await _employeeService.GetEmployeeAsync(employeeId);
            if (result.Code == HttpStatusCode.NotFound)
                return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeForCreationDto creationDto)
        {
            var result = await _employeeService.CreateEmployeeAsync(creationDto);
            if (result.Code == HttpStatusCode.BadRequest)
                return BadRequest(result.Message);
            if (result.Code == HttpStatusCode.InternalServerError)
                return StatusCode(500, result.Message);
            return Ok(result.ResultObj);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(string employeeId)
        {
            var result = await _employeeService.DeleteEmployeeAsync(employeeId);
            if (result.Code == HttpStatusCode.NotFound)
                return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
