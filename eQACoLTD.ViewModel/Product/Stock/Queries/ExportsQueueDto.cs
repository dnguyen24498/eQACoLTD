using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Stock.Queries
{
    public class ExportsQueueDto
    {
        public string OrderId { get; set; }
        public DateTime DateCreated { get; set; }
        public string TransactionStatusName { get; set; }
        public string EmployeeName { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string Description { get; set; }
    }
}
