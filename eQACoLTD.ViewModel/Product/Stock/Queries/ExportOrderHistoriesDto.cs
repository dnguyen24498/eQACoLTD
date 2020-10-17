using System;
using System.Collections;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.Product.Stock.Queries
{
    public class ExportOrderHistoriesDto
    {
        public string Id { get; set; }
        public DateTime ExportDate { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public string StockActionName { get; set; }
        public string WarehouseName { get; set; }
        public IEnumerable<ExportOrderHistoryDetailsDto> ListProduct { get; set; }
    }
}