using System;
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

        public async Task<ApiResult<CashBookReportDto>> GetCashBookReport(DateTime fromDate, DateTime toDate, int pageIndex, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/reports/cash-book?fromDate={fromDate:MM/dd/yyyy}&toDate={toDate:MM/dd/yyyy}&pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<CashBookReportDto>>(await response.Content.ReadAsStringAsync()
                );
            }
            return new ApiResult<CashBookReportDto>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResult<StockBookReportDto>> GetStockBookReport(DateTime dateTime, int pageIndex, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/reports/stock-book?dateTime={dateTime:MM/dd/yyyy}&pageIndex={pageIndex}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<StockBookReportDto>>(await response.Content.ReadAsStringAsync()
                );
            }
            return new ApiResult<StockBookReportDto>(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}