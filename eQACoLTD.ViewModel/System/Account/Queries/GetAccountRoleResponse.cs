using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.System.Role.Queries;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class GetAccountRoleResponse
    {
        public string UserName { get; set; }
        public List<RoleResponse> InRoles { get; set; }
        public List<RoleResponse> NotInRoles { get; set; }
    }
}
