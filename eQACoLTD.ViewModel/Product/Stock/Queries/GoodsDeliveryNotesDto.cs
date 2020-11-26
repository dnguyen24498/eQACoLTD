using System;

namespace eQACoLTD.ViewModel.Product.Stock.Queries
{
    public class GoodsDeliveryNotesDto
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public DateTime ExportDate { get; set; }
        public string EmployeeName { get; set; }
        public string StockActionId { get; set; }
        public string WarehouseName { get; set; }
        public string BranchName { get; set; }
    }
}