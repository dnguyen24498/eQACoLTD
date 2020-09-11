using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class OrderDetail
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ServiceName { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
        public StockHistory StockHistory { get; set; }
    }
}
