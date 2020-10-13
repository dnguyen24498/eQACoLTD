using eQACoLTD.Data.DBContext;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly AppIdentityDbContext _context;
        public SupplierService(AppIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<PagedResult<SuppliersDto>>> GetSuppliersPagingAsync(int pageIndex, int pageSize)
        {
            var suppliers=from s in _context.Suppliers
                          join employee in _context.Employees on s.Id equals employee.SupplierId
        }
    }
}
