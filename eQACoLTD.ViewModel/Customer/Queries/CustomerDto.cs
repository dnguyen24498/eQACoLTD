using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Customer.Queries
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public bool IsDelete { get; set; }
        public string CustomerTypeName { get; set; }
        public string Description { get; set; }
        public decimal TotalDebt { get; set; }
        public string EmployeeName { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
    }
}
