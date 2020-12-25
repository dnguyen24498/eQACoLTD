using System;

namespace eQACoLTD.ViewModel.Product.Payment.Queries
{
    public class PurchaseOrderPaymentsDto
    {
        public string Id { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal Paid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string EmployeeName { get; set; }
    }
}