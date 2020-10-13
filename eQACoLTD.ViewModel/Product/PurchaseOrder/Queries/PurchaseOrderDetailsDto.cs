using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.PurchaseOrder.Queries
{
    public class PurchaseOrderDetailsDto
    {
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string CostName { get; set; }
    }
}
