using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Stock.Handlers
{
    public class ExportOrderDto
    {
        private DateTime exportDate;
        public DateTime ExportDate { get=>exportDate;
            set
            {
                exportDate = value.ToLocalTime();
            }
        }
        public string Description { get; set; }
        public string StockActionId { get; set; }
        public string WarehouseId { get; set; }
    }
}
