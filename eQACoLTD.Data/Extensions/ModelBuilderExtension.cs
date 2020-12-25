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
                    RoleId = new Guid("b6a7f49c-ed4a-41bf-b2b3-9fdaca763459")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("1dcbb3b4-3bcd-4aaf-8b4d-e2339c5596f0"),
                    RoleId = new Guid("ae9c2256-44e4-4d46-a297-4da29c7e1637")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("2ac747da-3752-488d-87dc-cb5d4a2e9432"),
                    RoleId = new Guid("3b46cfe9-6b65-4a91-bdd9-9ec9052c422a")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("fee378c8-8a38-4b42-a420-82ff5888e819"),
                    RoleId = new Guid("a7148fa4-5a7c-4144-bbfd-6d72c4f191c6")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("94a967b5-914b-43a5-b7f5-2cd42d994b92"),
                    RoleId = new Guid("dabaaa26-81a6-4137-8534-428fcfe8f692")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("408e3e40-3191-451c-a606-a1f565310e8a"),
                    RoleId = new Guid("e70b5bc1-102a-4ba5-a4e1-dd75b1fe5b1b")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("ec3d3bc7-8141-4205-b068-4ca7d5fd1201"),
                    RoleId = new Guid("c4702302-748c-4b01-b0ad-8299d86896a4")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("567e24d4-8eaa-4e2f-95b9-10000aa55f07"),
                    RoleId = new Guid("b6a7f49c-ed4a-41bf-b2b3-9fdaca763459")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("0f2c7ea8-8c71-4459-b470-7eecf7493234"),
                    RoleId = new Guid("b6a7f49c-ed4a-41bf-b2b3-9fdaca763459")
                }
            );
            #endregion
            
        }
    }
}
