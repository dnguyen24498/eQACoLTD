using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Customer.Handlers
{
    public class CustomerAvatarRequest
    {
        public string CustomerId { get; set; }
        public IFormFile Imagefile { get; set; }
    }
}
