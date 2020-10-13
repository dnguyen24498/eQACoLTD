using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace eQACoLTD.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            
            var hasher=new PasswordHasher<AppUser>();
            builder.HasData(
                new AppUser()
                {
                    Id = new Guid("8a4bde2a-b1f9-4498-be84-6d0282573bcf"),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "dnguyen24498@gmail.com",
                    NormalizedEmail = "DNGUYEN24498@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null,"Bonghoatrang1@!"),
                    SecurityStamp = string.Empty
                },
                new AppUser()
                {
                    Id = new Guid("1dcbb3b4-3bcd-4aaf-8b4d-e2339c5596f0"),
                    UserName = "epn0001",
                    NormalizedUserName = "EPN0001",
                    Email = "duongnguyenadhp@gmail.com",
                    NormalizedEmail = "DUONGNGUYENADHP@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null,"Bonghoatrang1@!"),
                    SecurityStamp = string.Empty
                },
                new AppUser()
                {
                    Id = new Guid("2ac747da-3752-488d-87dc-cb5d4a2e9432"),
                    UserName = "epn0002",
                    NormalizedUserName = "EPN0002",
                    Email = "nguyen68973@st.vimaru.edu.vn",
                    NormalizedEmail = "NGUYEN68973@ST.VIMARU.EDU.VN",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null,"Bonghoatrang1@!"),
                    SecurityStamp = string.Empty
                },
                new AppUser()
                {
                    Id = new Guid("80efff0f-48cc-4e7a-8803-6782ce66960a"),
                    UserName = "cus0001",
                    NormalizedUserName = "CUS0001",
                    Email = "duongnguyenadhp1@gmail.com",
                    NormalizedEmail = "DUONGNGUYENADHP1@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null,"Bonghoatrang1@!"),
                    SecurityStamp = string.Empty
                },
                new AppUser()
                {
                    Id = new Guid("f502066f-7adc-4a5c-9d89-bb1015964cd9"),
                    UserName = "cus0002",
                    NormalizedUserName = "CUS0002",
                    Email = "duongnguyenadhp2@gmail.com",
                    NormalizedEmail = "DUONGNGUYENADHP2@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null,"Bonghoatrang1@!"),
                    SecurityStamp = string.Empty
                });
        }
    }
}
