using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Employee.Queries
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string DepartmentName { get; set; }
        public string BranchName { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        public string PhoneNumber { get; set; }
    }
}
