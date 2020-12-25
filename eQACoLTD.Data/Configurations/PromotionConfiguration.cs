using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class PromotionConfiguration:IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");
            builder.Property(x => x.DiscountValue).HasColumnType("decimal");
            builder.Property(x => x.DiscountType).HasColumnType("char(1)");
            builder.Property(x => x.FromDate).HasColumnType("datetime");
            builder.Property(x => x.ToDate).HasColumnType("datetime");
            builder.Property(x => x.CategoryId).IsRequired(false);

            builder.HasOne(c => c.Category)
                .WithMany(p => p.Promotions)
                .HasForeignKey(p => p.CategoryId);
            builder.HasData(new Promotion
            {
                Id = "116e249d-20f0-4fb0-a4eb-0ccd21ecdc31",
                Name = "Chương trình giảm giá Black Friday",
                Description =
                    "Chương trình diễn ra trong 7 ngày duy nhất từ 24/11-30/11, hãy nhanh tay mua sắm để nhận được mức giá ưu đãi cực hot lên đến 5% giá trị sản phẩm",
                FromDate = new DateTime(2020, 11, 24, 0, 0, 0),
                ToDate = new DateTime(2020, 11, 30, 23, 59, 59)
            });
        }
    }
}