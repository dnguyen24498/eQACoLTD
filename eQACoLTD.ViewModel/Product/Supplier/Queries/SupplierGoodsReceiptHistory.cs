using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Supplier.Queries
{
    public class SupplierGoodsReceiptHistory
    {
        public string PurchaseOrderId { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public int TotalRecord { get; set; }
    }
}
