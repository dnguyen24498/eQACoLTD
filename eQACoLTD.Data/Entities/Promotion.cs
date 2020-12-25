using System;
using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class Promotion
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public Category Category { get; set; }

        public List<PromotionDetail> PromotionDetails { get; set; }
        public List<CustomerPromotion> CustomerPromotions { get; set; }
        public List<Order> Orders { get; set; }

    }
}