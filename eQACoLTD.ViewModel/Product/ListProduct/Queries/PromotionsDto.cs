using System;

namespace eQACoLTD.ViewModel.Product.ListProduct.Queries
{
    public class PromotionsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NumberProduct { get; set; }
    }
}