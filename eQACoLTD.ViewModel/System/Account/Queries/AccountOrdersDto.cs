using System;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class AccountOrdersDto
    {
        public string OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public string TransactionStatus { get; set; }
    }
}