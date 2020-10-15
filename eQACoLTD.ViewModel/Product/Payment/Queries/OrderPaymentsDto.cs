using System;

namespace eQACoLTD.ViewModel.Product.Payment.Queries
{
    public class OrderPaymentsDto
    {
        public string Id { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal Received { get; set; }
        public DateTime PaymentDate { get; set; }
        public string EmployeeName { get; set; }
    }
}