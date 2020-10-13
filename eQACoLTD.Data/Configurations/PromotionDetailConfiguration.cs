using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class PromotionDetailConfiguration:IEntityTypeConfiguration<PromotionDetail>
    {
        public void Configure(EntityTypeBuilder<PromotionDetail> builder)
        {
            builder.ToTable("PromotionDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Promotion)
                .WithMany(p => p.PromotionDetails)
                .HasForeignKey(p => p.PromotionId);
            builder.HasOne(p => p.Product)
                .WithMany(p => p.PromotionDetails)
                .HasForeignKey(p => p.ProductId);
        }
    }
}