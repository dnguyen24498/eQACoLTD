using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Customer.Handlers
{
    public class CustomerRequest
    {
        public DateTime Dob { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public string CustomerTypeId { get; set; }
        public string DefaultPhoneNumber { get; set; }
    }
}
