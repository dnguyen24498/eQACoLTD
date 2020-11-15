using eQACoLTD.ViewModel.Common;

namespace eQACoLTD.ViewModel.Report.Queries
{
    public class StockBookReportDto
    {
        public int RealTotalInventory { get; set; }
        public decimal RealTotalInventoryValue { get; set; }
        public int SystemTotalInventory { get; set; }
        public decimal SystemTotalInventoryValue { get; set; }
        public PagedResult<StockBookRowReportDto> Rows { get; set; }
    }
}