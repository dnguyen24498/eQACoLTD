using eQACoLTD.Data.DBContext;
using eQACoLTD.ViewModel.Other.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;

namespace eQACoLTD.Application.Others
{
    public class OtherService : IOtherService
    {
        private readonly AppIdentityDbContext _context;
        public OtherService(AppIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BrandsForSelectionDto>> GetBrandsAsync()
        {
            var brands = await (from b in _context.Brands
                         select new BrandsForSelectionDto()
                         {
                             Id = b.Id,
                             Name=b.Name
                         }).ToListAsync();
            return brands;
        }
        
        public async Task<IEnumerable<CategoriesForSelectionDto>> GetCategoriesAsync()
        {
            var categories = await (from c in _context.Categories
                             select new CategoriesForSelectionDto()
                             {
                                 Id = c.Id,
                                 Name = c.Name
                             }).ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<CustomerTypesDto>> GetCustomertypesAsync()
        {
            var customerTypes = await (from ct in _context.CustomerTypes
                                 select new CustomerTypesDto()
                                 {
                                     Id = ct.Id,
                                     Name = ct.Name
                                 }).ToListAsync();
            return customerTypes;
        }

        public async Task<IEnumerable<EmployeesForSelection>> GetEmployeesAsync()
        {
            var employees = await (from e in _context.Employees
                                   where e.IsDelete==false
                                   select new EmployeesForSelection()
                                   {
                                       Id = e.Id,
                                       Name = e.Name
                                   }).ToListAsync();
            return employees;
        }
        public async Task<ApiResult<IEnumerable<WarehousesDto>>> GetWarehousesAsync(string employeeId)
        {
            var checkEmployee = await _context.Employees.FindAsync(employeeId);
            if(checkEmployee==null) return new ApiResult<IEnumerable<WarehousesDto>>(HttpStatusCode.NotFound);
            var warehouses = await (from w in _context.Warehouses
                where w.BranchId == checkEmployee.BranchId
                select new WarehousesDto()
                {
                    Id = w.Id,
                    Name = w.Name
                }).ToListAsync();
            return new ApiResult<IEnumerable<WarehousesDto>>(HttpStatusCode.OK,warehouses);

        }

        public async Task<IEnumerable<StockActionsDto>> GetStockActionsAsync()
        {
            var stockActions = await (from sa in _context.StockActions
                select new StockActionsDto()
                {
                    Id = sa.Id,
                    Name = sa.Name
                }).ToListAsync();
            return stockActions;
        }
    }
}
