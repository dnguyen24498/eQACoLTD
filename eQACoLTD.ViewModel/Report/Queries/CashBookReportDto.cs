using System.Collections.Generic;
using eQACoLTD.ViewModel.Common;

namespace eQACoLTD.ViewModel.Report.Queries
{
    public class CashBookReportDto
    {
        public decimal EndingStocksValue { get; set; }
        public decimal SurplusBeginningValue { get; set; }
        public decimal TotalReceivedValue { get; set; }
        public decimal TotalPaymentValue { get; set; }
        public PagedResult<CashBookRowReportDto> Rows { get; set; }
    }
}