using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.PurchaseOrder.Handlers
{
    public class PurchaseOrderForCreationDto
    {
        public string SupplierId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Description { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountType { get; set; }

        public IEnumerable<PurchaseOrderDetailsForCreation> ListProduct { get; set; }
    }
}
