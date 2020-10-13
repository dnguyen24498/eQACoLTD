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

            builder.HasOne(c => c.Category)
                .WithMany(p => p.Promotions)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}