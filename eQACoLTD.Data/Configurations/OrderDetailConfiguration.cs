using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class OrderDetailConfiguration:IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasColumnType("int");
            builder.Property(x => x.UnitPrice).HasColumnType("decimal");
            builder.Property(x => x.ServiceName).HasColumnType("nvarchar(300)");

            builder.HasOne(s => s.Order)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(s => s.OrderId);
            builder.HasOne(p => p.Product)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(s => s.ProductId);

            builder.HasData(
                new OrderDetail()
                {
                    Id = "ca9aa508-dc33-49b1-ae1c-8f76018dacdf",
                    OrderId = "SRN0001",
                    ProductId = "PRN0001",
                    Quantity = 1,
                    UnitPrice = 45000000
                });
        }
    }
}