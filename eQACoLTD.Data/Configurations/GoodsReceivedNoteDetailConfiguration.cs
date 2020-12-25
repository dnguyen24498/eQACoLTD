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
                    Id = "a2cf0535-7a25-4f2e-abfd-1a31d5352e4b",
                    GoodsReceivedNoteId = "GRN0001",
                    ProductId = "PRN0002",
                    Quantity = 10,
                    UnitPrice = 30000000
                },
                new GoodsReceivedNoteDetail()
                {
                    Id = "aa21c8ee-06ef-4451-bc7f-8f753cb99659",
                    GoodsReceivedNoteId = "GRN0001",
                    ProductId = "PRN0003",
                    Quantity = 10,
                    UnitPrice = 66000000
                },
                new GoodsReceivedNoteDetail()
                {
                    Id = "1c5c2a8f-8c5f-4cba-94d4-f640242af728",
                    GoodsReceivedNoteId = "GRN0002",
                    ProductId = "PRN0004",
                    Quantity = 10,
                    UnitPrice = 27590000
                },
                new GoodsReceivedNoteDetail()
                {
                    Id = "fdaa8695-f714-4aef-adce-e692124577fe",
                    GoodsReceivedNoteId = "GRN0002",
                    ProductId = "PRN0005",
                    Quantity = 10,
                    UnitPrice = 31990000
                },
                new GoodsReceivedNoteDetail()
                {
                    Id = "87ea5ecf-8f62-463b-8b9c-bad631f2fd9c",
                    GoodsReceivedNoteId = "GRN0002",
                    ProductId = "PRN0006",
                    Quantity = 10,
                    UnitPrice = 13990000
                },
                
                new GoodsReceivedNoteDetail()
                {
                    Id = "fc8bea22-d5ee-4fd2-a471-131fd74c3541",
                    GoodsReceivedNoteId = "GRN0003",
                    ProductId = "PRN0007",
                    Quantity = 10,
                    UnitPrice = 9990000
                },
                new GoodsReceivedNoteDetail()
                {
                    Id = "e3517ca9-5a15-458b-a73f-04bd44d0fea5",
                    GoodsReceivedNoteId = "GRN0003",
                    ProductId = "PRN0008",
                    Quantity = 10,
                    UnitPrice = 2990000
                },
                new GoodsReceivedNoteDetail()
                {
                    Id = "9456a727-ab5b-4698-b9e5-7a0634e2d6c3",
                    GoodsReceivedNoteId = "GRN0003",
                    ProductId = "PRN0010",
                    Quantity = 10,
                    UnitPrice = 280000
                },
                new GoodsReceivedNoteDetail()
                {
                    Id = "f3e34a02-0e2b-48aa-9705-67369dde43b9",
                    GoodsReceivedNoteId = "GRN0003",
                    ProductId = "PRN0011",
                    Quantity = 10,
                    UnitPrice = 280000
                },
                new GoodsReceivedNoteDetail()
                {
                    Id = "d29a204f-747d-4146-a9cc-6b49433bc8d9",
                    GoodsReceivedNoteId = "GRN0003",
                    ProductId = "PRN0016",
                    Quantity = 1,
                    UnitPrice = 22990000
                });
        }
    }
}