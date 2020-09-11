using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class AppRole:IdentityRole<Guid>
    {
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public AppUser AppUser { get; set; }

    }
}
