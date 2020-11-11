using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Report.Queries;
using Newtonsoft.Json;

namespace eQACoLTD.AdminMvc.Services
{
    public class ReportService:IReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<OverviewReport>> GetOverviewReport()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/reports/overview");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<OverviewReport>>(await response.Content.ReadAsStringAsync()
                );
            }
            return new ApiResult<OverviewReport>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}