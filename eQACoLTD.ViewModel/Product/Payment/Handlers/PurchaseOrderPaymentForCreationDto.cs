using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Payment.Handlers
{
    public class PurchaseOrderPaymentForCreationDto
    {
        public DateTime PaymentDate { get; set; }
        public decimal Paid { get; set; }
        public string PaymentMethodId { get; set; }
        public string Description { get; set; }
    }
}
