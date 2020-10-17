using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Stock.Handlers
{
    public class ExportOrderDto
    {
        public DateTime ExportDate { get; set; }
        public string Description { get; set; }
        public string StockActionId { get; set; }
        public string WarehouseId { get; set; }
    }
}
