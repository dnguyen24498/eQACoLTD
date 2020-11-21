using System;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.Product.Stock.Queries
{
    public class ImportPurchaseOrderHistoriesDto
    {
        public string Id { get; set; }
        public DateTime ImportDate { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public string StockActionName { get; set; }
        public string WarehouseName { get; set; }
        public IEnumerable<ImportPurchaseOrderHistoryDetailsDto> Products { get; set; }
    }
}