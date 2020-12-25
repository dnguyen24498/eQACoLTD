using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Order.Handlers
{
    public class OrderGoodsDeliveryNoteForCreationDto
    {
        public DateTime ExportDate { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public string StockActionId { get; set; }
        public string WarehouseId { get; set; }
    }
}
