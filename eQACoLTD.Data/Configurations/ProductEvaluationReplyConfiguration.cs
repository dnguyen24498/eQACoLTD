﻿using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class ProductEvaluationReplyConfiguration : IEntityTypeConfiguration<ProductEvaluationReply>
    {
        public void Configure(EntityTypeBuilder<ProductEvaluationReply> builder)
        {
            builder.ToTable("ProductEvaluationReplies");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).IsRequired().HasColumnType("nvarchar(1000)");

            builder.HasOne(pr => pr.ProductEvaluation)
                .WithMany(prd => prd.ProductReviewReplies)
                .HasForeignKey(prd => prd.ProductEvaluationId);
            builder.HasOne(a => a.AppUser)
                .WithMany(prd => prd.ProductEvaluationReplies)
                .HasForeignKey(prd => prd.AppUserId);

        }
    }
}
