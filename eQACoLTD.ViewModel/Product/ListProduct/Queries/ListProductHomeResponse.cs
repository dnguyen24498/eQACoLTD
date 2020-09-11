using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.ListProduct.Queries
{
    public class ListProductHomeResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal RetailPrice { get; set; }
        public int StarScore { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public int Views { get; set; }
        public string ImagePath { get; set; }
    }
}
