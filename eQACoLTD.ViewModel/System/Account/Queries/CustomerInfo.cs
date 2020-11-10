using System;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class CustomerInfo:AccountInfo    
    {
        public bool? Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
    }
}