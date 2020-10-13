using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Order.Queries
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string TransactionStatusName { get; set; }
        public string PaymentStatusName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public string EmployeeName { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountType { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RestAmount { get; set; }
        public string BranchName { get; set; }
        public IEnumerable<OrderDetailsDto> OrderDetailsDtos { get; set; }

    }
}
