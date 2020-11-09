using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class StockBookConfiguration:IEntityTypeConfiguration<StockBook>
    {
        public void Configure(EntityTypeBuilder<StockBook> builder)
        {
            builder.ToTable("StockBooks");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ImportQuantity).HasColumnType("int");
            builder.Property(x => x.ImportUnitPrice).HasColumnType("decimal").HasDefaultValue(0);
            builder.Property(x => x.ExportQuantity).HasColumnType("int");
            builder.Property(x => x.ExportUnitPrice).HasColumnType("decimal").HasDefaultValue(0);
            builder.Property(x => x.InventoryQuantity).HasColumnType("int");
            builder.Property(x => x.InventoryUnitPrice).HasColumnType("decimal").HasDefaultValue(0);

            builder.HasOne(g => g.GoodsReceivedNote)
                .WithOne(s => s.StockBook)
                .HasForeignKey<StockBook>(s => s.GoodsReceivedNoteId);
            
            builder.HasOne(g => g.GoodsDeliveryNote)
                .WithOne(s => s.StockBook)
                .HasForeignKey<StockBook>(s => s.GoodsDeliveryNoteId);
            
        }
    }
}