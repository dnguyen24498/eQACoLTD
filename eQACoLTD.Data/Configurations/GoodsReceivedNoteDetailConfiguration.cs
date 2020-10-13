using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class GoodsReceivedNoteDetailConfiguration:IEntityTypeConfiguration<GoodsReceivedNoteDetail>
    {
        public void Configure(EntityTypeBuilder<GoodsReceivedNoteDetail> builder)
        {
            builder.ToTable("GoodsReceivedNoteDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasColumnType("int").HasDefaultValue(1);
            builder.Property(x => x.UnitPrice).HasColumnType("decimal").HasDefaultValue(0);

            builder.HasOne(g => g.GoodsReceivedNote)
                .WithMany(g => g.GoodsReceivedNoteDetails)
                .HasForeignKey(g => g.GoodsReceivedNoteId);
            builder.HasOne(p => p.Product)
                .WithMany(g => g.GoodsReceivedNoteDetails)
                .HasForeignKey(g => g.ProductId);

            builder.HasData(
                new GoodsReceivedNoteDetail()
                {
                    Id = "80f3c6fc-71e3-42a6-90a0-be91f3bd5234",
                    GoodsReceivedNoteId = "GRN0001",
                    ProductId = "PRN0001",
                    Quantity = 10,
                    UnitPrice = 42000000
                },
                new GoodsReceivedNoteDetail()
                {
                    Id = "1c5c2a8f-8c5f-4cba-94d4-f640242af728",
                    GoodsReceivedNoteId = "GRN0001",
                    ProductId = "PRN0002",
                    Quantity = 10,
                    UnitPrice = 30000000
                });
        }
    }
}