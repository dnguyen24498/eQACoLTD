using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class ReturnDetailConfiguration:IEntityTypeConfiguration<ReturnDetail>
    {
        public void Configure(EntityTypeBuilder<ReturnDetail> builder)
        {
            builder.ToTable("ReturnDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasColumnType("int");
            builder.Property(x => x.UnitPrice).HasColumnType("decimal");
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");

            builder.HasOne(r => r.Return)
                .WithMany(r => r.ReturnDetails)
                .HasForeignKey(r => r.ReturnId);
            builder.HasOne(p => p.Product)
                .WithMany(r => r.ReturnDetails)
                .HasForeignKey(r => r.ProductId);
        }
    }
}