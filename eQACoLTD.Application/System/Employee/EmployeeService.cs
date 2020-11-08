using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.DBContext;
using eQACoLTD.Data.Entities;
using eQACoLTD.Utilities.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Employee.Handlers;
using eQACoLTD.ViewModel.System.Employee.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eQACoLTD.Application.System.Employee
{
    public class EmployeeService:IEmployeeService
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<AppRole> _roleManager;

        public EmployeeService(AppIdentityDbContext context, UserManager<AppUser> userManager, 
            ILoggerManager logger,IConfiguration configuration,RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<ApiResult<PagedResult<EmployeesDto>>> GetEmployeesPagingAsync(int pageIndex,int pageSize)
        {
            var employees = await (from employee in _context.Employees
                join branch in _context.Branches on employee.BranchId equals branch.Id
                    into BrachesGroup
                from b in BrachesGroup.DefaultIfEmpty()
                join department in _context.Departments on employee.DepartmentId equals department.Id
                    into DepartmentsGroup
                from d in DepartmentsGroup.DefaultIfEmpty()
                where employee.IsDelete == false
                select new EmployeesDto()
                {
                    Address = employee.Address,
                    Branch = b.Name,
                    Department = d.Name,
                    Id = employee.Id,
                    Name = employee.Name,
                    PhoneNumber = employee.PhoneNumber
                }).GetPagedAsync(pageIndex,pageSize);
            return new ApiResult<PagedResult<EmployeesDto>>(HttpStatusCode.OK,employees);
        }

        public async Task<ApiResult<EmployeeDto>> GetEmployeeAsync(string employeeId)
        {
            var employee = await (from e in _context.Employees
                join appuser in _context.AppUsers on e.AppuserId equals appuser.Id
                    into AppUserGroup
                from au in AppUserGroup.DefaultIfEmpty()
                join branch in _context.Branches on e.BranchId equals branch.Id
                    into BranchsGroup
                from b in BranchsGroup.DefaultIfEmpty()
                join department in _context.Departments on e.DepartmentId equals department.Id 
                    into DepartmentsGroup
                from d in DepartmentsGroup.DefaultIfEmpty()
                where e.Id==employeeId
                select new EmployeeDto()
                {
                    Id = e.Id,
                    Address = e.Address,
                    Description = e.Description,
                    Dob = e.Dob,
                    Gender = e.Gender,
                    Name = e.Name,
                    PhoneNumber = e.PhoneNumber,
                    UserName = au.UserName,
                    IsDelete = e.IsDelete,
                    DepartmentName = d.Name,
                    BranchName = b.Name
                }).SingleOrDefaultAsync();
            if(employee==null) 
                return new ApiResult<EmployeeDto>(HttpStatusCode.NotFound,$"Không tìm thấy nhân viên có mã: {employeeId}");
            return new ApiResult<EmployeeDto>(HttpStatusCode.OK,employee);
        }

        public async Task<ApiResult<string>> CreateEmployeeAsync(EmployeeForCreationDto creationDto)
        {
            try
            {
                if (creationDto != null)
                {
                    var checkRole = await _roleManager.FindByIdAsync(creationDto.DefaultRoleId.ToString("D"));
                    if (checkRole == null)
                    {
                        _logger.LogError($"Mã quyền {creationDto.DefaultRoleId} không tồn tại");
                        return new ApiResult<string>(HttpStatusCode.BadRequest,$"Mã quyền {creationDto.DefaultRoleId} không tồn tại");
                    }
                    var checkEmail = await _userManager.FindByEmailAsync(creationDto.Email);
                    if(checkEmail!=null) 
                        return new ApiResult<string>(HttpStatusCode.BadRequest,$"Email: {creationDto.Email} đã được sử dụng");
                    var countNumber = await _context.Employees.CountAsync();
                    var generateId = IdentifyGenerator.GenerateEmployeeId(countNumber + 1);
                    var employee = ObjectMapper.Mapper.Map<EmployeeForCreationDto, Data.Entities.Employee>(creationDto);
                    employee.Id = generateId;
                    employee.IsDelete = false;
                    await _context.Employees.AddAsync(employee);
                    await _context.SaveChangesAsync();
                    var newAccount=new AppUser(generateId.ToLower())
                    {
                        PhoneNumber = creationDto.PhoneNumber,
                        Email = creationDto.Email
                    };
                    var result=await _userManager.CreateAsync(newAccount, _configuration["DefaultPassword"]);
                    if (!result.Succeeded)
                    {
                        _logger.LogError("Có lỗi khi tạo tài khoản cho nhân viên");
                        return new ApiResult<string>(HttpStatusCode.InternalServerError,
                            "Có lỗi khi tạo tài khoản cho nhân viên");
                    }

                    var createdEmployee = await _context.Employees.Where(x => x.Id == generateId).SingleOrDefaultAsync();
                    if (createdEmployee != null)
                    {
                        var createdAccount = await _userManager.FindByNameAsync(newAccount.UserName);
                        createdEmployee.AppuserId = createdAccount.Id;
                        await _context.SaveChangesAsync();
                        await _userManager.AddToRoleAsync(createdAccount, checkRole.Name);
                        return new ApiResult<string>()
                        {
                            Code = HttpStatusCode.OK,
                            ResultObj = generateId,
                            Message = "Tạo mới nhân viên thành công"
                        };
                    }
                    _logger.LogError($"Nhân viên có mã {generateId} chưa được tạo.");
                    return new ApiResult<string>(HttpStatusCode.InternalServerError,$"Nhân viên có mã {generateId} chưa được tạo.");
                }
                return new ApiResult<string>(HttpStatusCode.BadRequest,$"Employee is null");
            }
            catch (Exception e)
            {
               _logger.LogError($"Có lỗi khi tạo nhân viên, lỗi: {e.Message}");
               return new ApiResult<string>(HttpStatusCode.InternalServerError,"Có lỗi khi tạo tài khoản cho nhân viên");
            }
        }

        public async Task<ApiResult<string>> DeleteEmployeeAsync(string employeeId)
        {
            var checkEmp = await _context.Employees.FindAsync(employeeId);
            if (checkEmp == null)
            {
                _logger.LogInfo($"Không tìm thấy nhân viên có mã: {employeeId}");
                return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy nhân viên có mã: {employeeId}");
            }
            checkEmp.IsDelete = true;
            await _context.SaveChangesAsync();
            var employeeAccount = await _userManager.FindByIdAsync(checkEmp.AppuserId.ToString());
            if (employeeAccount != null)
            {
                await _userManager.SetLockoutEnabledAsync(employeeAccount, true);
                await _userManager.SetLockoutEndDateAsync(employeeAccount, new DateTimeOffset(new DateTime(9999, 1, 1)));
            }
            return new ApiResult<string>(HttpStatusCode.OK)
            {
                ResultObj = employeeId,
                Message = "Xóa nhân viên thành công"
            };
        }
    }
}