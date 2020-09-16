using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.ToTable("ProductReviews");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnType("nvarchar(100)");
            builder.Property(x => x.Content).HasColumnType("nvarchar(500)");
            builder.Property(x => x.StarScore).HasColumnType("tinyint").HasDefaultValue(1);

            builder.HasOne(p => p.Product)
                .WithMany(pr => pr.ProductReviews)
                .HasForeignKey(pr => pr.ProductId);
            builder.HasOne(a => a.AppUser)
                .WithMany(pr => pr.ProductReviews)
                .HasForeignKey(pr => pr.UserId);

        }
    }
}
