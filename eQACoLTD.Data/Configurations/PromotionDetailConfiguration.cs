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
            builder.Property(x => x.DiscountType).HasColumnType("char(1)");
            builder.Property(x => x.DiscountValue).HasColumnType("decimal");

            builder.HasOne(p => p.Promotion)
                .WithMany(p => p.PromotionDetails)
                .HasForeignKey(p => p.PromotionId);
            builder.HasOne(p => p.Product)
                .WithMany(p => p.PromotionDetails)
                .HasForeignKey(p => p.ProductId);

            builder.HasData(new PromotionDetail()
            {
                Id = "8b54beba-2bf8-4e37-85bf-134dcc972ae9",
                PromotionId = "116e249d-20f0-4fb0-a4eb-0ccd21ecdc31",
                ProductId = "PRN0001",
                DiscountType = "%",
                DiscountValue = 2
            },
                new PromotionDetail()
                {
                    Id = "bf3a0587-59a8-4ebc-a1ca-48332ef3759e",
                    PromotionId = "116e249d-20f0-4fb0-a4eb-0ccd21ecdc31",
                    ProductId = "PRN0002",
                    DiscountType = "%",
                    DiscountValue = 3
                },new PromotionDetail()
                {
                    Id = "b2d5d729-88b5-4354-a19c-9866ca1c8e4b",
                    PromotionId = "116e249d-20f0-4fb0-a4eb-0ccd21ecdc31",
                    ProductId = "PRN0003",
                    DiscountType = "%",
                    DiscountValue = 2
                },new PromotionDetail()
                {
                    Id = "3b183700-eee0-4166-81d7-44e84ea79e46",
                    PromotionId = "116e249d-20f0-4fb0-a4eb-0ccd21ecdc31",
                    ProductId = "PRN0016",
                    DiscountType = "%",
                    DiscountValue = 2
                });
        }
    }
}