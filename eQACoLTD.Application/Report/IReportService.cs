using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Queries;
using eQACoLTD.ViewModel.Product.Supplier.Queries;

namespace eQACoLTD.Application.Report
{
    public interface IReportService
    {
        Task<ApiResult<PagedResult<CustomerDto>>> GetAllCustomerDebtAsync(int pageIndex,int pageSize);
        Task<ApiResult<PagedResult<SupplierDto>>> GetAllSupplierDebtAsync(int pageIndex,int pageSize);
        
    }
}