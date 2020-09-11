using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Category.Queries
{
    public class CategoryResponse
    {
        public string Id { get; set; }
        public string ThumbnailImagePath { get; set; }
        public string Name { get; set; }
        public int NumbProduct { get; set; }
        public string Description { get; set; }
        public int TotalRecord { get; set; }
    }
}
