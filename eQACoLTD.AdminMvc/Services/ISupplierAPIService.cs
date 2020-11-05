using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Supplier.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface ISupplierAPIService
    {
        Task<ApiResult<PagedResult<SuppliersDto>>> GetSupplierPagingAsync(int pageIndex,int pageSize);
        
    }
}