using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class CustomerPromotionConfiguration:IEntityTypeConfiguration<CustomerPromotion>
    {
        public void Configure(EntityTypeBuilder<CustomerPromotion> builder)
        {
            builder.ToTable("CustomerPromotions");
            builder.HasKey(x => new {x.CustomerId, x.PromotionId});
            builder.Property(x => x.DiscountValue).HasColumnType("decimal");
            builder.Property(x => x.DiscountType).HasColumnType("char(1)");
            builder.Property(x => x.Code).HasColumnType("char(32)");

            builder.HasOne(c => c.Customer)
                .WithMany(c => c.CustomerPromotions)
                .HasForeignKey(c => c.CustomerId);
            builder.HasOne(p => p.Promotion)
                .WithMany(c => c.CustomerPromotions)
                .HasForeignKey(c => c.PromotionId);
        }
    }
}