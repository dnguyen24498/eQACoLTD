using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class GoodsDeliveryNoteConfiguration:IEntityTypeConfiguration<GoodsDeliveryNote>
    {
        public void Configure(EntityTypeBuilder<GoodsDeliveryNote> builder)
        {
            builder.ToTable("GoodsDeliveryNotes");
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ExportDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");

            builder.HasOne(s => s.Order)
                .WithMany(g => g.GoodsDeliveryNotes)
                .HasForeignKey(g => g.OrderId);
            
            builder.HasOne(e => e.Employee)
                .WithMany(g => g.GoodsDeliveryNotes)
                .HasForeignKey(g => g.EmployeeId);
            builder.HasOne(s => s.StockAction)
                .WithMany(g => g.GoodsDeliveryNotes)
                .HasForeignKey(g => g.StockActionId);
            builder.HasOne(w => w.Warehouse)
                .WithMany(g => g.GoodsDeliveryNotes)
                .HasForeignKey(g => g.WarehouseId);
            builder.HasOne(r => r.Return)
                .WithMany(g => g.GoodsDeliveryNotes)
                .HasForeignKey(g => g.ReturnId);
            builder.HasOne(r => r.RepairVoucher)
                .WithMany(g => g.GoodsDeliveryNotes)
                .HasForeignKey(g => g.RepairVoucherId);
            builder.HasOne(l => l.LiquidationVoucher)
                .WithMany(g => g.GoodsDeliveryNotes)
                .HasForeignKey(g => g.LiquidationVoucherId);

            builder.HasData(
                new GoodsDeliveryNote()
                {
                    Id = "GDN0001",
                    OrderId = "SRN0001",
                    ExportDate = DateTime.Now,
                    EmployeeId = "EPN0001",
                    StockActionId = "e27503bd-12c6-4d8e-a68e-6296892134e2",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5"
                });
        }
    }
}