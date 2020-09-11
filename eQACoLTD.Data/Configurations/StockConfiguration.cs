using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");
            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.Inventory).HasDefaultValue(0);
            builder.Property(x => x.AbleToSale).HasDefaultValue(0);

            builder.HasOne(p => p.Product)
                .WithOne(s => s.Stock)
                .HasForeignKey<Stock>(s => s.ProductId);
        }
    }
}
