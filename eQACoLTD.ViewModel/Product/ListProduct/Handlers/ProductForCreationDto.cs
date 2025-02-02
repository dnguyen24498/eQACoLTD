﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace eQACoLTD.ViewModel.Product.ListProduct.Handlers
{
    public class ProductForCreationDto
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrices { get; set; }
        public int WarrantyPeriod { get; set; }
        public string BrandId { get; set; }
        public string OverView { get; set; }
    }
}
