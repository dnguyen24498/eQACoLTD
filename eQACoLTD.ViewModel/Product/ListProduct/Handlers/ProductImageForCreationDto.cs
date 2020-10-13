using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.ListProduct.Handlers
{
    public class ProductImageForCreationDto
    {
        public IFormFile Image { get; set; }
        public int IsThumbnail { get; set; } = 0;
    }
}
