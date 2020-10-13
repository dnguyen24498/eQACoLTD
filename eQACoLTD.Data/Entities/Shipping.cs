using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class ShippingOrder
    {
        public string SalesReceiptId { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Fee { get; set; }
        public string TransporterId { get; set; }
        public string Address { get; set; }
        public string ShippingStatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public string LiquidationVoucherId { get; set; }

        public Order Order { get; set; }
        public Customer Customer { get; set; }
        public Transporter Transporter { get; set; }
        public ShippingStatus ShippingStatus { get; set; }
    }
}
