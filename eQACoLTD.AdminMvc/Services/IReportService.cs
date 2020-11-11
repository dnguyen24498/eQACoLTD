using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Report.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IReportService
    {
        Task<ApiResult<OverviewReport>> GetOverviewReport();
    }
}