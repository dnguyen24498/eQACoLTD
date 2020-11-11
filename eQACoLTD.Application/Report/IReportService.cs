using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Queries;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using eQACoLTD.ViewModel.Report.Queries;

namespace eQACoLTD.Application.Report
{
    public interface IReportService
    {
        Task<ApiResult<ReportCustomerSupplierDebtDto>> GetAllCustomerDebtAsync(int pageIndex,int pageSize);
        Task<ApiResult<ReportCustomerSupplierDebtDto>> GetAllSupplierDebtAsync(int pageIndex,int pageSize);
        Task<ApiResult<OverviewReport>> GetOverviewReport(string accountId);
    }
}