using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class ProductReviewReplyConfiguration : IEntityTypeConfiguration<ProductReviewReply>
    {
        public void Configure(EntityTypeBuilder<ProductReviewReply> builder)
        {
            builder.ToTable("ProductReviewReplies");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).IsRequired().HasColumnType("nvarchar(1000)");

            builder.HasOne(pr => pr.ProductReview)
                .WithMany(prd => prd.ProductReviewReplies)
                .HasForeignKey(prd => prd.ProductReviewId);
            builder.HasOne(a => a.AppUser)
                .WithMany(prd => prd.ProductReviewReplies)
                .HasForeignKey(prd => prd.UserId);

        }
    }
}
