using System;

namespace eQACoLTD.ViewModel.System.Account.Handlers
{
    public class AccountForUpdateDto
    {
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Fax { get; set; }
    }
}