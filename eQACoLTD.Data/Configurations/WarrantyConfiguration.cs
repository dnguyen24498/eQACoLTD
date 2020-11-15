using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class WarrantyConfiguration:IEntityTypeConfiguration<Warranty>
    {
        public void Configure(EntityTypeBuilder<Warranty> builder)
        {
            builder.ToTable("Warranties");
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.PurchaseDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Description).HasDefaultValue("nvarchar(300)");

            builder.HasOne(s => s.Order)
                .WithOne(w => w.Warranty)
                .HasForeignKey<Warranty>(w => w.OrderId);
            builder.HasOne(e => e.Employee)
                .WithMany(w => w.Warranties)
                .HasForeignKey(w => w.EmployeeId);


        }
    }
}