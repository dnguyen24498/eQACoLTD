using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.User.Queries
{
    public class UserProfileResponse
    {
        public DateTime DOB { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }

    }
}
