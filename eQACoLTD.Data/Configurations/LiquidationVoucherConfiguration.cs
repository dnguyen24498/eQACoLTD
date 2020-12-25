using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class LiquidationVoucherConfiguration:IEntityTypeConfiguration<LiquidationVoucher>
    {
        public void Configure(EntityTypeBuilder<LiquidationVoucher> builder)
        {
            builder.ToTable("LiquidationVouchers");
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");
            builder.Property(x => x.LiquidationDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.DiscountType).HasColumnType("char(1)");
            builder.Property(x => x.DiscountValue).HasColumnType("decimal");
            builder.Property(x => x.CustomerName).HasColumnType("nvarchar(200)");
            builder.Property(x => x.CustomerAddress).HasColumnType("nvarchar(300)");
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(30)");

            builder.HasOne(c => c.Customer)
                .WithMany(l => l.LiquidationVouchers)
                .HasForeignKey(l => l.CustomerId);
            builder.HasOne(w => w.Warehouse)
                .WithMany(l => l.LiquidationVouchers)
                .HasForeignKey(l => l.WarehouseId);
            builder.HasOne(e => e.Employee)
                .WithMany(l => l.LiquidationVouchers)
                .HasForeignKey(l => l.EmployeeId);
        }
    }
}