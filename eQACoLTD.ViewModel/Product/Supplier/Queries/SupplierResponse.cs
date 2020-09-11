using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Supplier.Queries
{
    public class SupplierResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalRecord { get; set; }
    }
}
