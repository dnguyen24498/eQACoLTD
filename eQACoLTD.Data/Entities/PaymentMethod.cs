using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class PaymentMethod
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<PaymentVoucher> PaymentVouchers { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
    }
}
