using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Employee.Handlers
{
    public class EmployeeForCreationDto
    {
        public DateTime Dob { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string BranchId { get; set; }
        public string DepartmentId { get; set; }
        public Guid DefaultRoleId { get; set; }
        
    }
}
