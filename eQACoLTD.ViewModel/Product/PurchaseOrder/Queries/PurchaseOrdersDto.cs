using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.PurchaseOrder.Queries
{
    public class PurchaseOrdersDto
    {
        public string Id { get; set; }
        public string SupplierName { get; set; }
        public string TransactionStatusName { get; set; }
        public string PaymentStatusName { get; set; }
        public DateTime DateCreated { get; set; }
        public string EmployeeName { get; set; }
    }
}
