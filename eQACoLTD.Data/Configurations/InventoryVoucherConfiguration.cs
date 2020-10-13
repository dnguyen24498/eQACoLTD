using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class InventoryVoucherConfiguration:IEntityTypeConfiguration<InventoryVoucher>
    {
        public void Configure(EntityTypeBuilder<InventoryVoucher> builder)
        {
            builder.ToTable("InventoryVouchers");
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.InventoryDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsConfirm).HasColumnType("bit").HasDefaultValue(0);

            builder.HasOne(e => e.Employee)
                .WithMany(i => i.InventoryVouchers)
                .HasForeignKey(i => i.EmployeeId);
            builder.HasOne(w => w.Warehouse)
                .WithMany(i => i.InventoryVouchers)
                .HasForeignKey(i => i.WarehouseId);
        }
    }
}