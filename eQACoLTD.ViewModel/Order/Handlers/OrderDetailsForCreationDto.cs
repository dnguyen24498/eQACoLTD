using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Order.Handlers
{
    public class OrderDetailsForCreationDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ServiceName { get; set; }
    }
}
