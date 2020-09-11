using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class StockHistory
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public DateTime RecordDate { get; set; }
        public string EmployeeId { get; set; }
        public string StockActionId { get; set; }
        public int ChangeQuantity { get; set; }
        public string PurchaseOrderDetailId { get; set; }
        public string OrderDetailId { get; set; }

        public Product Product { get; set; }
        public Employee Employee { get; set; }
        public StockAction StockAction { get; set; }
        public PurchaseOrderDetail PurchaseOrderDetail { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
