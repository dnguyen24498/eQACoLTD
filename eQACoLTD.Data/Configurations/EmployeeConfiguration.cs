using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.Dob).HasDefaultValue(new DateTime(1990, 1, 1));
            builder.Property(x => x.Name).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.Address).HasColumnType("nvarchar(300)");
            builder.Property(x => x.Gender).HasDefaultValue(false);
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(30)");
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");

            builder.HasOne(au => au.AppUser)
                .WithOne(e => e.Employee)
                .HasForeignKey<Employee>(e => e.AppuserId);
            builder.HasOne(d => d.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.DepartmentId);
            builder.HasOne(b => b.Branch)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.BranchId);

            builder.HasData(
                new Employee()
                {
                    Id = "EPN0001",
                    Dob = new DateTime(1998,04,24),
                    Name = "Nguyễn Thanh Tùng",
                    Address = "Số 88, Trần Thành Ngọ, Kiến An, Hải Phòng",
                    Gender = true,
                    IsDelete = false,
                    AppuserId = new Guid("1dcbb3b4-3bcd-4aaf-8b4d-e2339c5596f0"),
                    DepartmentId = "023536ea-f6c4-40a1-a610-2e95dd0e4f2a",
                    PhoneNumber = "0934349618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Employee()
                {
                    Id = "EPN0002",
                    Dob = new DateTime(1998,04,24),
                    Name = "Nguyễn Thanh Tú",
                    Address = "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng",
                    Gender = true,
                    IsDelete = false,
                    AppuserId = new Guid("2ac747da-3752-488d-87dc-cb5d4a2e9432"),
                    DepartmentId = "023536ea-f6c4-40a1-a610-2e95dd0e4f2a",
                    PhoneNumber = "0934347618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                });
        }
    }
}
