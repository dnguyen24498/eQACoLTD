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
            builder.Property(x => x.Adrress).HasColumnType("nvarchar(300)");
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
                    Id = new Guid("567e24d4-8eaa-4e2f-95b9-10000aa55f07"),
                    UserName = "epn0008",
                    NormalizedUserName = "EPN0008",
                    Email = "nicholasnguyen5798@gmail.com",
                    NormalizedEmail = "NICHOLASNGUYEN5798@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null,"Bonghoatrang1@!"),
                    SecurityStamp = string.Empty  
                },
                new AppUser()
                {
                    Id = new Guid("0f2c7ea8-8c71-4459-b470-7eecf7493234"),
                    UserName = "epn0009",
                    NormalizedUserName = "EPN0009",
                    Email = "nguyenthanhtungkahp1998@gmail.com",
                    NormalizedEmail = "NGUYENTHANHTUNGKAHP1998@GMAIL.COM",
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
                    Id = new Guid("fee378c8-8a38-4b42-a420-82ff5888e819"),
                    UserName = "epn0003",
                    NormalizedUserName = "EPN0003",
                    Email = "duongnguyenadhp3@gmail.com",
                    NormalizedEmail = "DUONGNGUYENADHP3@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                    SecurityStamp = string.Empty
                },
                new AppUser()
                {
                    Id = new Guid("94a967b5-914b-43a5-b7f5-2cd42d994b92"),
                    UserName = "epn0004",
                    NormalizedUserName = "EPN0004",
                    Email = "duongnguyenadhp4@gmail.com",
                    NormalizedEmail = "DUONGNGUYENADHP4@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                    SecurityStamp = string.Empty
                },
                new AppUser()
                {
                    Id = new Guid("408e3e40-3191-451c-a606-a1f565310e8a"),
                    UserName = "epn0005",
                    NormalizedUserName = "EPN0005",
                    Email = "duongnguyenadhp5@gmail.com",
                    NormalizedEmail = "DUONGNGUYENADHP5@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                    SecurityStamp = string.Empty
                },
                new AppUser()
                {
                    Id = new Guid("ec3d3bc7-8141-4205-b068-4ca7d5fd1201"),
                    UserName = "epn0006",
                    NormalizedUserName = "EPN0006",
                    Email = "duongnguyenadhp6@gmail.com",
                    NormalizedEmail = "DUONGNGUYENADHP6@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
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
