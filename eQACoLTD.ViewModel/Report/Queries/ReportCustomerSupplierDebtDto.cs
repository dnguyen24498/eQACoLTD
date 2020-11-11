using System.Collections.Generic;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Queries;

namespace eQACoLTD.ViewModel.Report.Queries
{
    public class ReportCustomerSupplierDebtDto
    {
        public decimal? TotalDebt { get; set; }
        public PagedResult<CustomersSuppliersForReportDto> Targets { get; set; }
    }
}