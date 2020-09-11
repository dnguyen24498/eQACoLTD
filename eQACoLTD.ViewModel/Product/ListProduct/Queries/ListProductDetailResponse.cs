using eQACoLTD.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.ListProduct.Queries
{
    public class ListProductDetailResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public int StarScore { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrices { get; set; }
        public int WarrantyPeriod { get; set; }
        public string BrandName { get; set; }
    }
}
