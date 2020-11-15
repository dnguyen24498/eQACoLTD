namespace eQACoLTD.ViewModel.Report.Queries
{
    public class StockBookRowReportDto
    {
        public string ProductName { get; set; }
        public string Id { get; set; }
        public int RealInventoryQuantity { get; set; } = 0;
        public decimal AveragePrice { get; set; } = 0m;
        public int SystemInventoryQuantity { get; set; } = 0;
        public decimal TotalInventoryValue { get=>RealInventoryQuantity*AveragePrice; }
        public decimal TotalSystemValue { get=>SystemInventoryQuantity*AveragePrice; }
    }
}