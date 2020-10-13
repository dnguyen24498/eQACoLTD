using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Employee.Handlers
{
    public class PutEmployeeRequest
    {
        public DateTime Dob { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public string DefaultPhoneNumber { get; set; }
    }
}
