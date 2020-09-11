using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class PaymentVoucher
    {
        public string Id { get; set; }
        public string PurchaseOrderId { get; set; }
        public decimal Paid { get; set; }
        public DateTime PayDate { get; set; }
        public bool IsDelete { get; set; }
        public string PaymentMethodId { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string SupplierId { get; set; }

        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
