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

        public async Task<IEnumerable<CustomerTypesDto>> GetCustomerTypesAsync()
        {
            var customerTypes = await (from ct in _context.CustomerTypes
                                 select new CustomerTypesDto()
                                 {
                                     Id = ct.Id,
                                     Name = ct.Name
                                 }).ToListAsync();
            return customerTypes;
        }
        public async Task<ApiResult<IEnumerable<WarehousesDto>>> GetWarehousesAsync()
        {
            var warehouses = await (from w in _context.Warehouses
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

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethod()
        {
            var paymentMethod = await _context.PaymentMethods.ToListAsync();
            return paymentMethod;
        }
    }
}
