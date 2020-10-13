using eQACoLTD.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eQACoLTD.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seeding(this ModelBuilder modelBuilder)
        {
           
            #region AppUserRoles
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("8a4bde2a-b1f9-4498-be84-6d0282573bcf"),
                    RoleId = new Guid("1e76986e-fad7-42d9-a689-8a69d36273f9")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("1dcbb3b4-3bcd-4aaf-8b4d-e2339c5596f0"),
                    RoleId = new Guid("a7148fa4-5a7c-4144-bbfd-6d72c4f191c6")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("2ac747da-3752-488d-87dc-cb5d4a2e9432"),
                    RoleId = new Guid("3b46cfe9-6b65-4a91-bdd9-9ec9052c422a")
                }
            );
            #endregion
            
        }
    }
}
