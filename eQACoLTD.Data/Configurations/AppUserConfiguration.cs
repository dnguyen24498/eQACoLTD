using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.MiddleName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);
            builder.Property(x => x.City).HasMaxLength(50);
            builder.Property(x => x.District).HasMaxLength(100);
            builder.Property(x => x.SubDistrict).HasMaxLength(100);
            builder.Property(x => x.Street).HasMaxLength(100);
            builder.Property(x => x.Address).HasMaxLength(200);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(20);
        }
    }
}
