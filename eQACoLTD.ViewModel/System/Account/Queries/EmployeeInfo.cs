using System;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class EmployeeInfo:AccountInfo
    {
        public bool? Gender { get; set; }
        public DateTime Dob { get; set; }
        public string DepartmentName { get; set; }
        public string BranchName { get; set; }
    }
}