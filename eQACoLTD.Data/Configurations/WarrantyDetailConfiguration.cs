using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class WarrantyDetailConfiguration:IEntityTypeConfiguration<WarrantyDetail>
    {
        public void Configure(EntityTypeBuilder<WarrantyDetail> builder)
        {
            builder.ToTable("WarrantyDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.WarrantyPeriods).HasColumnType("int");

            builder.HasOne(p => p.Product)
                .WithMany(w => w.WarrantyDetails)
                .HasForeignKey(w => w.ProductId);
            builder.HasOne(w => w.Warranty)
                .WithMany(w => w.WarrantyDetails)
                .HasForeignKey(w => w.WarrantyId);
        }
    }
}