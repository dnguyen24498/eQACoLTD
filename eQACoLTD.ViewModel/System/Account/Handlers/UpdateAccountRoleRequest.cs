using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eQACoLTD.ViewModel.System.Account.Handlers
{
    public class UpdateAccountRoleRequest
    {
        public List<string> AddRoleNames { get; set; }
        public List<string> DeleteRoleNames { get; set; }
    }
}
