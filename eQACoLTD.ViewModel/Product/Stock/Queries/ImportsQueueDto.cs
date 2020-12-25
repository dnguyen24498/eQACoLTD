using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Stock.Queries
{
    public class ImportsQueueDto
    {
        public string PurchaseOrderId { get; set; }
        public DateTime ImportDate { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public string RepairVoucherId { get; set; }
        public string ReturnId { get; set; }
        public string TransactionStatusName { get; set; }
        public string BranchName { get; set; }
    }
}
