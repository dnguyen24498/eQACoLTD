using Dapper;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Employee.Handlers;
using eQACoLTD.ViewModel.System.Employee.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public EmployeeService(IConfiguration configuration,UserManager<AppUser> userManager
            ,RoleManager<AppRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task DeleteEmployeeAsync(string employeeId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                if (await CheckEmployeeAsync(employeeId) != null)
                {
                    await connection.ExecuteAsync($"UPDATE Employees SET IsDelete=1 WHERE Id='{employeeId}'");
                    var employeeAccount = await _userManager.FindByNameAsync(employeeId);
                    if (employeeAccount != null)
                        await _userManager.SetLockoutEnabledAsync(employeeAccount, true);
                }
            }
        }

        public async Task<ApiResult<EmployeeDetailResponse>> GetEmployeeAsync(string employeeId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                if (await CheckEmployeeAsync(employeeId) != null)
                {
                    var employee = await connection.QueryFirstOrDefaultAsync<EmployeeDetailResponse>
                        (@"EXEC prGetEmployeeById @employeeId=@EmployeeId",
                        new { EmployeeId = employeeId });
                    return new ApiSuccessResult<EmployeeDetailResponse>(employee);
                }
                return new ApiErrorResult<EmployeeDetailResponse>("Không tìm thấy nhân viên");
            }
        }

        public async Task<ApiResult<PagedResult<EmployeeResponse>>> GetEmployeesPagingAsync(int pageIndex)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var employees = await connection.QueryAsync<EmployeeResponse>
                    (@"EXEC prGetEmployeePaging @pageNumber=@PageNumber,@rowsOfPage=@RowsOfPage",
                    new { PageNumber = pageIndex, RowsOfPage = int.Parse(_configuration["PageSize"]) });
                return new ApiSuccessResult<PagedResult<EmployeeResponse>>
                    (employees.MapPage(pageIndex, int.Parse(_configuration["PageSize"]),
                    employees.Count()>0?employees.ElementAt(0).TotalRecord:0));
            }
        }

        public async Task<ApiResult<string>> PostEmployeeAsync(PostEmployeeRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var sequenceNumber = await connection.QueryFirstOrDefaultAsync<int>
                    (@"SELECT COUNT(*) FROM Employees");
                string id=IdentifyGenerator.GenerateEmployeeId(sequenceNumber + 1);
                var newEmployeeAccount = new AppUser(id.ToLower()) { Id = Guid.NewGuid(),
                    PhoneNumber = request.DefaultPhoneNumber ??= "",
                    Email=string.IsNullOrEmpty(request.Email)?"":request.Email};
                var createAccountResult = await _userManager.CreateAsync(newEmployeeAccount, _configuration["DefaultPassword"]);
                if (!createAccountResult.Succeeded)
                    return new ApiErrorResult<string>("Tạo tài khoản cho nhân viên thất bại");
                var query = $"INSERT INTO Employees(Id,Dob,FullName,Address,Gender,IsDelete,UserName,DefaultPhoneNumber) VALUES(" +
                    $"'{id}','{request.Dob.ToString("yyyy/MM/dd")}',N'{request.FullName}',N'{request.Address}'," +
                    $"{request.Gender},0,'{newEmployeeAccount.UserName}','{request.DefaultPhoneNumber}')";
                await connection.ExecuteAsync(query);
                return new ApiSuccessResult<string>(id);
            }
        }

        public async Task<ApiResult<EmployeeDetailResponse>> PutEmployeeAsync(string employeeId, PutEmployeeRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                if (await CheckEmployeeAsync(employeeId) != null)
                {
                    var query = $"UPDATE Employees SET Dob='{request.Dob.ToString("yyyy/MM/dd")}'," +
                        $"FullName=N'{request.FullName}',Address=N'{request.Address}',Gender={request.Gender}," +
                        $"DefaultPhoneNumber='{request.DefaultPhoneNumber}' WHERE Id='{employeeId}'";
                    await connection.ExecuteAsync(query);
                    return new ApiSuccessResult<EmployeeDetailResponse>(await CheckEmployeeAsync(employeeId));
                }
            }
            return new ApiErrorResult<EmployeeDetailResponse>("Không tìm thấy nhân viên");
        }

        private async Task<EmployeeDetailResponse> CheckEmployeeAsync(string employeeId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var employee = await connection.QueryFirstOrDefaultAsync<EmployeeDetailResponse>
                    (@"EXEC prGetEmployeeById @employeeId=@EmployeeId",new {EmployeeId=employeeId });
                if (employee != null) return employee;
                return null;
            }
        }
    }
}
