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
            builder.Property(x => x.Address).IsRequired().HasColumnType("nvarchar(300)");
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(30)");
            builder.Property(x => x.Email).HasColumnType("varchar(100)");
            builder.Property(x => x.Fax).HasColumnType("varchar(30)");
            builder.Property(x => x.Website).HasColumnType("varchar(150)");
            builder.Property(x => x.Description).HasColumnType("varchar(250)");
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.EmployeeId).IsRequired(false).HasDefaultValue();

            builder.HasOne(e => e.Employee)
                .WithMany(sup => sup.Suppliers)
                .HasForeignKey(sup => sup.EmployeeId);
        }
    }
}
