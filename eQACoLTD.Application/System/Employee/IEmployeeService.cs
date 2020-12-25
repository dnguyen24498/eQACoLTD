using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Employee.Handlers;
using eQACoLTD.ViewModel.System.Employee.Queries;

namespace eQACoLTD.Application.System.Employee
{
    public interface IEmployeeService
    {
        Task<ApiResult<PagedResult<EmployeesDto>>> GetEmployeesPagingAsync(int pageIndex,int pageSize);
        Task<ApiResult<EmployeeDto>> GetEmployeeAsync(string employeeId);
        Task<ApiResult<string>> CreateEmployeeAsync(EmployeeForCreationDto creationDto);
        Task<ApiResult<string>> DeleteEmployeeAsync(string employeeId);
    }
}