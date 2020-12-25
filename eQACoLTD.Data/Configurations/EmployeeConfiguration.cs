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
                    DepartmentId = "15d805f8-32fb-43f3-8c95-31e87c05d3e7",
                    PhoneNumber = "0934347618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Employee()
                {
                    Id = "EPN0003",
                    Dob = new DateTime(1998,04,24),
                    Name = "Nguyễn Văn An",
                    Address = "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng",
                    Gender = true,
                    IsDelete = false,
                    AppuserId = new Guid("fee378c8-8a38-4b42-a420-82ff5888e819"),
                    DepartmentId = "023536ea-f6c4-40a1-a610-2e95dd0e4f2a",
                    PhoneNumber = "0934347618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Employee()
                {
                    Id = "EPN0004",
                    Dob = new DateTime(1998,04,24),
                    Name = "Nguyễn Thành Nam",
                    Address = "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng",
                    Gender = true,
                    IsDelete = false,
                    AppuserId = new Guid("94a967b5-914b-43a5-b7f5-2cd42d994b92"),
                    DepartmentId = "896571ab-344a-4258-b144-3f5c39b286e1",
                    PhoneNumber = "0934347618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Employee()
                {
                    Id = "EPN0005",
                    Dob = new DateTime(1998,04,24),
                    Name = "Nguyễn Khánh Ly",
                    Address = "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng",
                    Gender = false,
                    IsDelete = false,
                    AppuserId = new Guid("408e3e40-3191-451c-a606-a1f565310e8a"),
                    DepartmentId = "896571ab-344a-4258-b144-3f5c39b286e1",
                    PhoneNumber = "0934347618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Employee()
                {
                    Id = "EPN0006",
                    Dob = new DateTime(1998,04,24),
                    Name = "Trần Thị Thủy",
                    Address = "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng",
                    Gender = false,
                    IsDelete = false,
                    AppuserId = new Guid("ec3d3bc7-8141-4205-b068-4ca7d5fd1201"),
                    DepartmentId = "2c33ac5e-c033-4eeb-b185-7f79007bbec3",
                    PhoneNumber = "0934347618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Employee()
                {
                    Id = "EPN0007",
                    Dob = new DateTime(1998,04,24),
                    Name = "Nguyễn Dương Nguyên",
                    Address = "Số 88-Hải Triều-Quán Toan-Hồng Bàng-Hải Phòng",
                    Gender = true,
                    IsDelete = false,
                    AppuserId = new Guid("8a4bde2a-b1f9-4498-be84-6d0282573bcf"),
                    DepartmentId = "8567cb14-668c-45f9-9c9a-182ee3b99981",
                    PhoneNumber = "0934347618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Employee()
                {
                    Id = "EPN0008",
                    Dob = new DateTime(1998,04,24),
                    Name = "Nguyễn Thanh Tú",
                    Address = "Tôn Đức Thắng, Hải Phòng",
                    Gender = true,
                    IsDelete = false,
                    AppuserId = new Guid("567e24d4-8eaa-4e2f-95b9-10000aa55f07"),
                    DepartmentId = "8567cb14-668c-45f9-9c9a-182ee3b99981",
                    PhoneNumber = "0934347618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Employee()
                {
                    Id = "EPN0009",
                    Dob = new DateTime(1998,04,24),
                    Name = "Nguyễn Thanh Tùng",
                    Address = "Trần Thành Ngọ, Kiến An, Hải Phòng",
                    Gender = true,
                    IsDelete = false,
                    AppuserId = new Guid("0f2c7ea8-8c71-4459-b470-7eecf7493234"),
                    DepartmentId = "8567cb14-668c-45f9-9c9a-182ee3b99981",
                    PhoneNumber = "0934347618",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                });
        }
    }
}
