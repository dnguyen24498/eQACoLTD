using System;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.Product.ListProduct.Queries
{
    public class PromotionForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public IEnumerable<PromotionDetailForCreationDto> Products { get; set; }
    }
}