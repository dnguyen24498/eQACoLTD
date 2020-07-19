using eQACoLTD.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seeding(this ModelBuilder modelBuilder)
        {
            var roleId = new Guid("D41440DF-BFC5-4599-BA94-6B43E8E1ED26");
            var adminId = new Guid("4DCE65A4-01AB-4A3C-9D3F-090AB2803C60");

            var roleId2 = new Guid("A5479D8A-70D6-47B2-8DDA-20F6C67341EC");
            var userId = new Guid("E90F766A-F9B2-4280-AE28-411053AF3891");


            modelBuilder.Entity<AppRole>().HasData(new AppRole()
            {
                Id = roleId2,
                Name = "Salesman",
                NormalizedName = "Salesman",
                Description = "Quyền nhân viên bán hàng"
            });


            modelBuilder.Entity<AppRole>().HasData(new AppRole()
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "Admin",
                Description = "Quyền quản trị viên"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser()
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "dnguyen24498@gmail.com",
                NormalizedEmail = "dnguyen24498@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                SecurityStamp = string.Empty,
                FirstName = "Nguyễn",
                MiddleName = "Dương",
                LastName = "Nguyên",
                DOB = new DateTime(1998, 04, 24),
                City = "Hải Phòng",
                District = "Hồng Bàng",
                SubDistrict = "Quán Toan",
                Street = "Hải triều",
                Address = "Số nhà 88",
                Gender = true,
            });

            modelBuilder.Entity<AppUser>().HasData(new AppUser()
            {
                Id = userId,
                UserName = "dnguyen244",
                NormalizedUserName = "dnguyen244",
                Email = "dnguyen244@gmail.com",
                NormalizedEmail = "dnguyen244@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                SecurityStamp = string.Empty,
                FirstName = "Nguyễn",
                MiddleName = "Dương",
                LastName = "Nguyên",
                DOB = new DateTime(1998, 04, 24),
                City = "Hải Phòng",
                District = "Hồng Bàng",
                SubDistrict = "Quán Toan",
                Street = "Hải triều",
                Address = "Số nhà 88",
                Gender = true,
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId2,
                UserId = userId
            });
        }
    }
}
