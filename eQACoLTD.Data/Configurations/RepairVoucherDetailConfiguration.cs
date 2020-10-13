using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class RepairVoucherDetailConfiguration:IEntityTypeConfiguration<RepairVoucherDetail>
    {
        public void Configure(EntityTypeBuilder<RepairVoucherDetail> builder)
        {
            builder.ToTable("RepairVoucherDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasColumnType("int");
            builder.Property(x => x.UnitPrice).HasColumnType("decimal");
            builder.Property(x => x.RepairContent).HasColumnType("nvarchar(500)");
            builder.Property(x => x.ProductName).HasColumnType("nvarchar(200)");
            builder.Property(x => x.IsFixed).HasColumnType("bit").HasDefaultValue(0);

            builder.HasOne(r => r.RepairVoucher)
                .WithMany(r => r.RepairVoucherDetails)
                .HasForeignKey(r => r.RepairVoucherId);
            builder.HasOne(p => p.Product)
                .WithMany(r => r.RepairVoucherDetails)
                .HasForeignKey(r => r.ProductId);
            
        }
    }
}