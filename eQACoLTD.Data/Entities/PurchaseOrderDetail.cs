using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class PurchaseOrderDetail
    {
        public string Id { get; set; }
        public string PurchaseOrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string CostName { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
        public Product Product { get; set; }
        public StockHistory StockHistory { get; set; }
    }
}
