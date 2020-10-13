using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.PurchaseOrder.Handlers
{
    public class PurchaseOrderDetailsForCreation
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string CostName { get; set; }
    }
}
