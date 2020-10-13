using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Employee.Queries
{
    public class EmployeeDetailResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string AvatarPath { get; set; }
    }
}
