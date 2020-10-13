using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Supplier.Queries
{
    public class SupplierImportHistoriesDto
    {
        public string PurchaseOrderId { get; set; }
        public string TransactionStatus { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public string BranchName { get; set; }
    }
}
