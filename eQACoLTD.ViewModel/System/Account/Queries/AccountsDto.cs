using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class AccountsDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmployeeName { get; set; }
        public string CustomerName { get; set; }
        public string SupplierName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
