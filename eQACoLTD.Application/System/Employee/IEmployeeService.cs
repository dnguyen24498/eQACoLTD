using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Employee.Handlers;
using eQACoLTD.ViewModel.System.Employee.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Employee
{
    public interface IEmployeeService
    {
        Task<ApiResult<EmployeeDetailResponse>> GetEmployeeAsync(string employeeId);
        Task<ApiResult<PagedResult<EmployeeResponse>>> GetEmployeesPagingAsync(int pageIndex);
        Task<ApiResult<string>> PostEmployeeAsync(PostEmployeeRequest request);
        Task DeleteEmployeeAsync(string employeeId);
        Task<ApiResult<EmployeeDetailResponse>> PutEmployeeAsync(string employeeId, PutEmployeeRequest request);
    }
}
