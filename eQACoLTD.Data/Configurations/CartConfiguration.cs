using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(x => new { x.UserId, x.ProductId });
            builder.Property(x => x.Quantity).HasDefaultValue(1);

            builder.HasOne(a => a.AppUser)
                .WithMany(c => c.Carts)
                .HasForeignKey(c => c.UserId);
            builder.HasOne(p => p.Product)
                .WithMany(c => c.Carts)
                .HasForeignKey(c => c.ProductId);
        }
    }
}
