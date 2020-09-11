using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(x => x.Information).HasColumnType("nvarchar(500)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(4000)");
            builder.Property(x => x.Views).HasDefaultValue(0);
            builder.Property(x => x.RetailPrice).HasDefaultValue(0);
            builder.Property(x => x.WholesalePrices).HasDefaultValue(0);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.CategoryId).IsRequired(false).HasDefaultValue();
            builder.Property(x => x.BrandId).IsRequired(false).HasDefaultValue();
            builder.Property(x => x.StarScore).HasDefaultValue(0);
            builder.Property(x => x.WarrantyPeriod).HasDefaultValue(0);

            builder.HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);
            builder.HasOne(b => b.Brand)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.BrandId);
        }
    }
}
