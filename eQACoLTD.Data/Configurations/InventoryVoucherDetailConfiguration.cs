using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class InventoryVoucherDetailConfiguration:IEntityTypeConfiguration<InventoryVoucherDetail>
    {
        public void Configure(EntityTypeBuilder<InventoryVoucherDetail> builder)
        {
            builder.ToTable("InventoryVoucherDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SystemQuantity).HasColumnType("int");
            builder.Property(x => x.RealQuantity).HasColumnType("int");
            builder.Property(x => x.BadQuantity).HasColumnType("int");
            builder.Property(x => x.NormalQuantity).HasColumnType("int");
            builder.Property(x => x.ExpiredQuantity).HasColumnType("int");

            builder.HasOne(i => i.InventoryVoucher)
                .WithMany(i => i.InventoryVoucherDetails)
                .HasForeignKey(i => i.InventoryVoucherId);
            builder.HasOne(i => i.Product)
                .WithMany(i => i.InventoryVoucherDetails)
                .HasForeignKey(i => i.ProductId);   
        }
    }
}