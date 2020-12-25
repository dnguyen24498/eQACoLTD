using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Employee.Handlers
{
    public class EmployeeForUpdateDto
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string Department { get; set; }
        public string Branch { get; set; }
        public string Description { get; set; }
        public string IsDelete { get; set; }
        public string PhoneNumber { get; set; }
    }
}
