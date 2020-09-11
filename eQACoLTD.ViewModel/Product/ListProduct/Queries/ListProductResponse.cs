using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.ListProduct.Queries
{
    public class ListProductResponse
    {
        public string ThumbnailPath { get; set; }
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public int AbleToSale { get; set; }
        public int TotalRecord { get; set; }
    }
}
