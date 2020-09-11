using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Category.Handlers
{
    public class CategoryImageRequest
    {
        public string CategoryId { get; set; }
        public IFormFile Imagefile { get; set; }
    }
}
