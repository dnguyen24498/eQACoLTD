using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Order.Handlers
{
    public class OrderForCreationDto
    {
        public string CustomerId { get; set; }
        public string Description { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountType { get; set; }
        public IEnumerable<OrderDetailsForCreationDto> ListOrderDetail { get; set; }
    }
}
