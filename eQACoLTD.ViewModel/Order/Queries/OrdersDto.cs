using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Order.Queries
{
    public class OrdersDto
    {
        public string OrderId { get; set; }
        public DateTime DateCreated { get; set; }
        public string TransactionStatusName { get; set; }
        public string PaymentStatusName { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
    }
}
