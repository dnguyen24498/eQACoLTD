using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.Dob).HasDefaultValue(new DateTime(1990, 1, 1));
            builder.Property(x => x.Name).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.Address).HasColumnType("nvarchar(300)");
            builder.Property(x => x.Gender).HasDefaultValue(false);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(30)");
            builder.Property(x => x.AppUserId).IsRequired(false);
            builder.Property(x => x.Email).HasColumnType("varchar(200)");
            builder.Property(x => x.Fax).HasColumnType("varchar(50)");
            builder.Property(x => x.Website).HasColumnType("nvarchar(200)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");
            

            builder.HasOne(au => au.AppUser)
                .WithOne(c => c.Customer)
                .HasForeignKey<Customer>(c => c.AppUserId);
            builder.HasOne(ct => ct.CustomerType)
                .WithMany(c => c.Customers)
                .HasForeignKey(c => c.CustomerTypeId);
            builder.HasOne(e => e.Employee)
                .WithMany(c => c.Customers)
                .HasForeignKey(c => c.EmployeeId);

            builder.HasData(
                new Customer()
                {
                    Id = "CUS0001",
                    Name = "Nguyễn Dương Nguyên",
                    Address = "Số 88, Đường Hải Triều, Phường Quán Toan, Quận Hồng Bàng, Thành phố Hải Phòng",
                    Gender = true,
                    IsDelete = false,
                    AppUserId = new Guid("80efff0f-48cc-4e7a-8803-6782ce66960a"),
                    CustomerTypeId = "a3bc8a51-9264-4590-af51-5fd20812695a",
                    PhoneNumber = "0934349618",
                    Email = "duongnguyenadhp@gmail.com"
                },
                new Customer()
                {
                    Id = "CUS0002",
                    Name = "Bùi Thùy Dương",
                    Address = "Số 99, Đường Hải Triều, Phường Quán Toan, Quận Hồng Bàng, Thành phố Hải Phòng",
                    Gender = false,
                    IsDelete = false,
                    AppUserId = new Guid("f502066f-7adc-4a5c-9d89-bb1015964cd9"),
                    CustomerTypeId = "a3bc8a51-9264-4590-af51-5fd20812695a",
                    PhoneNumber = "0934349618",
                    Email = "duongnguyenadhp@gmail.com"
                });
        }
    }
}
