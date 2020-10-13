using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Customer.Handlers
{
    public class CustomerForCreationDto
    {
        public DateTime Dob { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public string CustomerTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string EmployeeId { get; set; }
    }
}
