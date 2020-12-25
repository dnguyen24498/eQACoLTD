using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Payment.Handlers
{
    public class PurchaseOrderPaymentForCreationDto
    {
        private DateTime paymentDate;
        public DateTime PaymentDate { 
            get=>paymentDate;
            set
            {
                paymentDate = value.ToLocalTime();
            }
        }
        public decimal Paid { get; set; }
        public string PaymentMethodId { get; set; }
        public string Description { get; set; }
    }
}
