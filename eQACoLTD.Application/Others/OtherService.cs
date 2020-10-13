using eQACoLTD.Data.DBContext;
using eQACoLTD.ViewModel.Other.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
    }
}
