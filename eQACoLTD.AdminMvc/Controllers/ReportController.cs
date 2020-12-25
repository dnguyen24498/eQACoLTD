using System;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Report.Queries;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class ReportController : Controller
    {
        private IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        // GET
        public async Task<IActionResult> CashBook(DateTime fromDate, DateTime toDate, int page=1,int size=50)
        {
            var result = await _reportService.GetCashBookReport(fromDate, toDate, page, size);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if(result.Code!=HttpStatusCode.OK) return View(new CashBookReportDto());
            return View(result.ResultObj);
        }

        public async Task<IActionResult> StockBook(DateTime dateTime, int page = 1, int size = 50)
        {
            var result = await _reportService.GetStockBookReport(dateTime, page, size);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if (result.Code != HttpStatusCode.OK) return View(new StockBookReportDto());
            return View(result.ResultObj);
        }
    }
}