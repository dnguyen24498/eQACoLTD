using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class LiquidationVoucherDetailConfiguration:IEntityTypeConfiguration<LiquidationVoucherDetail>
    {
        public void Configure(EntityTypeBuilder<LiquidationVoucherDetail> builder)
        {
            builder.ToTable("LiquidationVoucherDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasColumnType("int");
            builder.Property(x => x.UnitPrice).HasColumnType("decimal");

            builder.HasOne(l => l.LiquidationVoucher)
                .WithMany(l => l.LiquidationVoucherDetails)
                .HasForeignKey(l => l.LiquidationVoucherId);
            builder.HasOne(p => p.Product)
                .WithMany(l => l.LiquidationVoucherDetails)
                .HasForeignKey(l => l.ProductId);
        }
    }
}