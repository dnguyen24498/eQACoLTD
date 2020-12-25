using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public List<AccountRolesDto> InRoles { get; set; }
    }
}
