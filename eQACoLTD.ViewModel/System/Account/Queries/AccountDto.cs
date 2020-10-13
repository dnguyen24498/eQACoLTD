using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class AccountDetailResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public string FullName { get; set; }

        public List<AccountRolesDto> InRoles { get; set; }
    }
}
