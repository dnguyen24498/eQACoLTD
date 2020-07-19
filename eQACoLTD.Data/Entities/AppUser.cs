using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        public AppUser(){}
        public AppUser(string userName)
        {
            this.UserName = userName;
        }
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
    }
}
