using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class GoodwReceivedNoteConfiguration:IEntityTypeConfiguration<GoodsReceivedNote>
    {
        public void Configure(EntityTypeBuilder<GoodsReceivedNote> builder)
        {
            builder.ToTable("GoodReceivedNotes");
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImportDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");
            builder.Property(x => x.PlacedLocation).HasColumnType("nvarchar(300)");
        }
    }
}