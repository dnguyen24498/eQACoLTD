using System;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.Order.Queries
{
    public class WaitingOrderDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalAmount { get; set; }
        
        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
    }
}