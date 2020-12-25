using System;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Report.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IReportService
    {
        Task<ApiResult<OverviewReport>> GetOverviewReport();
        Task<ApiResult<CashBookReportDto>> GetCashBookReport(DateTime fromDate, DateTime toDate,int pageIndex, int pageSize);
        Task<ApiResult<StockBookReportDto>> GetStockBookReport(DateTime dateTime, int pageIndex, int pageSize);
    }
}