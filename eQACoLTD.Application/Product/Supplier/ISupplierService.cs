using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.Supplier
{
    public interface ISupplierService
    {
        Task<ApiResult<PagedResult<SuppliersDto>>> GetSuppliersPagingAsync(int pageIndex, int pageSize);
    }
}
