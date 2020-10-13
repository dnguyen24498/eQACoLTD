using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class GoodsDeliveryNoteDetailConfiguration:IEntityTypeConfiguration<GoodsDeliveryNoteDetail>
    {
        public void Configure(EntityTypeBuilder<GoodsDeliveryNoteDetail> builder)
        {
            builder.ToTable("GoodsDeliveryNoteDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UnitPrice).HasColumnType("decimal");
            builder.Property(x => x.Quantity).HasColumnType("int");

            builder.HasOne(g => g.GoodsDeliveryNote)
                .WithMany(g => g.GoodsDeliveryNoteDetails)
                .HasForeignKey(g => g.GoodsDeliveryNoteId);
            builder.HasOne(p => p.Product)
                .WithMany(g => g.GoodsDeliveryNoteDetails)
                .HasForeignKey(g => g.ProductId);

            builder.HasData(
                new GoodsDeliveryNoteDetail()
                {
                    Id = "d1013190-9ca9-4ef0-a0a3-1df43601fa9c",
                    GoodsDeliveryNoteId = "GDN0001",
                    ProductId = "PRN0001",
                    Quantity = 1,
                    UnitPrice = 45000000
                });
        }
    }
}