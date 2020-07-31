using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.System.Role.Queries;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class AccountRolesVM
    {
        public string UserName { get; set; }
        public List<string> InRoles { get; set; }
        public List<string> NotInRoles { get; set; }
    }
}
