using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRoles");
            builder.Property(x => x.Description).HasColumnType("nvarchar(250)");

            builder.HasData(
                new AppRole()
                {
                    Id = new Guid("1e76986e-fad7-42d9-a689-8a69d36273f9"),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Description = "Quyền quản trị viên"
                },
                new AppRole()
                {
                    Id = new Guid("a7148fa4-5a7c-4144-bbfd-6d72c4f191c6"),
                    Name = "WarehouseStaff",
                    NormalizedName = "WAREHOUSESTAFF",
                    Description = "Quyền nhân viên kho"
                },
                new AppRole()
                {
                    Id = new Guid("3b46cfe9-6b65-4a91-bdd9-9ec9052c422a"),
                    Name = "Receptionist",
                    NormalizedName = "RECEPTIONIST",
                    Description = "Quyền nhân viên lễ tân"
                },
                new AppRole()
                {
                    Id = new Guid("dabaaa26-81a6-4137-8534-428fcfe8f692"),
                    Name = "Cashier",
                    NormalizedName = "CASHIER",
                    Description = "Quyền nhân viên thu ngân"
                }
                
            );
        }
    }
}
