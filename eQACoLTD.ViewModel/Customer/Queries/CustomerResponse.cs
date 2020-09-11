using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Customer.Queries
{
    public class CustomerResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullAddress { get; set; }
        public string CustomerTypeName { get; set; }
        public bool IsDelete { get; set; }
        public int TotalRecord { get; set; }
    }
}
