using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Address).HasColumnType("nvarchar(300)");
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(30)");
            builder.Property(x => x.Email).HasColumnType("varchar(150)");
            builder.Property(x => x.Fax).HasColumnType("varchar(30)");
            builder.Property(x => x.Website).HasColumnType("varchar(150)");
            builder.Property(x => x.Description).HasColumnType("varchar(250)");
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.EmployeeId).IsRequired(false).HasDefaultValue();
            builder.Property(x => x.AppUserId).IsRequired(false);

            builder.HasOne(e => e.Employee)
                .WithMany(sup => sup.Suppliers)
                .HasForeignKey(sup => sup.EmployeeId);
            builder.HasOne(u => u.AppUser)
                .WithOne(s => s.Supplier)
                .HasForeignKey<Supplier>(s => s.AppUserId);

            builder.HasData(
                new Supplier()
                {
                    Id = "SUN0001",
                    Name = "Công ty TNHH Minh Khang",
                    Address = "Kinh Môn, Hải Dương",
                    PhoneNumber = "0959842342",
                    Email = "minhkhangconntact@gmail.com",
                    EmployeeId = "EPN0001",
                    IsDelete = false
                },
                new Supplier()
                {
                    Id = "SUN0002",
                    Name = "Công ty TNHH Thiên An",
                    Address = "An Lão, Hải Phòng",
                    PhoneNumber = "0959842342",
                    Email = "thienanconntact@gmail.com",
                    EmployeeId = "EPN0002",
                    IsDelete = false
                },
                new Supplier()
                {
                    Id = "SUN0003",
                    Name = "Cửa hàng máy tính Laptop247",
                    Address = "Số 102,Nguyễn Bình, Đổng Quốc Bình, Ngô Quyền, Hải Phòng",
                    PhoneNumber = "0959842342",
                    Email = "laptop247conntact@gmail.com",
                    EmployeeId = "EPN0001",
                    IsDelete = false
                },
                new Supplier()
                {
                    Id = "SUN0004",
                    Name = "Cửa hàng máy tính Hải Phòng",
                    Address = "Số 109,Nguyễn Bình, Đổng Quốc Bình, Ngô Quyền, Hải Phòng",
                    PhoneNumber = "0959842342",
                    Email = "laptophaiphongconntact@gmail.com",
                    EmployeeId = "EPN0002",
                    IsDelete = false
                });
        }
    }
}
