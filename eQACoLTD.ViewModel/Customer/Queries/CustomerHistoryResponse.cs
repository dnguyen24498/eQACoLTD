using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Customer.Queries
{
    public class CustomerHistoryResponse
    {
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingStatus { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalRecord { get; set; }
    }
}
