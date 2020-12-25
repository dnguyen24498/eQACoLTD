using System;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.Product.ListProduct.Queries
{
    public class PromotionDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public IEnumerable<PromotionDetailDto> Products { get; set; }
        
    }
}