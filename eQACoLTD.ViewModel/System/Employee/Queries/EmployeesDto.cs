using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Employee.Queries
{
    public class EmployeeResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDelete { get; set; }
        public int TotalRecord { get; set; }
    }
}
