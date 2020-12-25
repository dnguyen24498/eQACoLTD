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
                    Description = "Quản trị viên"
                },
                new AppRole()
                {
                    Id = new Guid("a7148fa4-5a7c-4144-bbfd-6d72c4f191c6"),
                    Name = "WarehouseStaff",
                    NormalizedName = "WAREHOUSESTAFF",
                    Description = "Nhân viên kho"
                },
                new AppRole()
                {
                    Id = new Guid("3b46cfe9-6b65-4a91-bdd9-9ec9052c422a"),
                    Name = "Salesman",
                    NormalizedName = "SALESMAN",
                    Description = "Nhân viên bán hàng"
                },
                new AppRole()
                {
                    Id = new Guid("dabaaa26-81a6-4137-8534-428fcfe8f692"),
                    Name = "Cashier",
                    NormalizedName = "CASHIER",
                    Description = "Nhân viên thu ngân"
                },
                new AppRole()
                {
                    Id = new Guid("ae9c2256-44e4-4d46-a297-4da29c7e1637"),
                    Name = "WarehouseManager",
                    NormalizedName = "WAREHOUSEMANAGER",
                    Description = "Thủ kho"
                },
                new AppRole()
                {
                    Id = new Guid("e70b5bc1-102a-4ba5-a4e1-dd75b1fe5b1b"),
                    Name = "CashManager",
                    NormalizedName = "CASHMANAGER",
                    Description = "Thủ quỹ"
                },
                new AppRole()
                {
                    Id = new Guid("c4702302-748c-4b01-b0ad-8299d86896a4"),
                    Name = "BusinessStaff",
                    NormalizedName = "BUSINESSSTAFF",
                    Description = "Nhân viên kinh doanh"
                },
                new AppRole()
                {
                    Id = new Guid("68113af4-39b0-4926-b0fb-091d827fc6d9"),
                    Name = "Technician",
                    NormalizedName = "TECHNICIAN",
                    Description = "Nhân viên kỹ thuật"
                },
                new AppRole()
                {
                    Id = new Guid("0ae13bb5-43f5-404e-9100-046e7ff0bfc7"),
                    Name = "Accountant",
                    NormalizedName = "ACCOUNTANT",
                    Description = "Kế toán"
                },
                new AppRole()
                {
                    Id = new Guid("b6a7f49c-ed4a-41bf-b2b3-9fdaca763459"),
                    Name = "SuperAdministrator",
                    NormalizedName = "SUPERADMINISTRATOR",
                    Description = "Giám đốc"
                },
                new AppRole()
                {
                    Id = new Guid("2c3047ca-da34-4a37-a640-d8b20bf0f21c"),
                    Name = "Manager",
                    NormalizedName = "MANAGER",
                    Description = "Quản lý chi nhánh"
                }

            );
        }
    }
}
