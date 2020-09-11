using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class ProductImage
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ImagePath { get; set; }
        public string FullPath { get; set; }
        public bool IsThumbnail { get; set; }

        public Product Product { get; set; }
    }
}
