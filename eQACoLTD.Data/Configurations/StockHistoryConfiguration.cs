using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class StockHistoryConfiguration : IEntityTypeConfiguration<StockHistory>
    {
        public void Configure(EntityTypeBuilder<StockHistory> builder)
        {
            builder.ToTable("StockHistories");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RecordDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ChangeQuantity).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired(false).HasDefaultValue();
            builder.Property(x => x.PurchaseOrderDetailId).IsRequired(false).HasDefaultValue();
            builder.Property(x => x.OrderDetailId).IsRequired(false).HasDefaultValue();

            builder.HasOne(p => p.Product)
                .WithMany(sh => sh.StockHistories)
                .HasForeignKey(sh => sh.ProductId);
            builder.HasOne(e => e.Employee)
                .WithMany(sh => sh.StockHistories)
                .HasForeignKey(sh => sh.EmployeeId);
            builder.HasOne(sa => sa.StockAction)
                .WithMany(sh => sh.StockHistories)
                .HasForeignKey(sh => sh.StockActionId);
            builder.HasOne(po => po.PurchaseOrderDetail)
                .WithOne(sh => sh.StockHistory)
                .HasForeignKey<StockHistory>(sh => sh.PurchaseOrderDetailId);
            builder.HasOne(o => o.OrderDetail)
                .WithOne(sh => sh.StockHistory)
                .HasForeignKey<StockHistory>(sh => sh.OrderDetailId);
        }
    }
}
