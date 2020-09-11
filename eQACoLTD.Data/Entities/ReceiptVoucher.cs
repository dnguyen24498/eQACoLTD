using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class ReceiptVoucher
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public decimal Received { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string PaymentMethodId { get; set; }
        public bool IsDelete { get; set; }
        public string Description { get; set; }
        public string SupplierId { get; set; }
        public string CustomerId { get; set; }


        public Supplier Supplier { get; set; }
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
