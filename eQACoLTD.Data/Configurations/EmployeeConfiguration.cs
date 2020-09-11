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
            builder.Property(x => x.FullName).HasColumnType("nvarchar(200)").HasDefaultValue();
            builder.Property(x => x.Address).HasColumnType("nvarchar(300)").HasDefaultValue();
            builder.Property(x => x.Gender).HasDefaultValue();
            builder.Property(x => x.DefaultPhoneNumber).HasColumnType("varchar(30)").HasDefaultValue();
            builder.Property(x => x.IsDelete).HasDefaultValue(false);

            builder.HasOne(au => au.AppUser)
                .WithOne(e => e.Employee)
                .HasForeignKey<Employee>(e => e.UserId);
        }
    }
}
