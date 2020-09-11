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
            builder.Property(x => x.UserName).HasColumnType("varchar(14)");
            builder.Property(x => x.DateCreated).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Email).HasColumnType("varchar(150)");
            builder.Property(x => x.EmailConfirmed).HasColumnType("varchar(150)");
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(30)");
        }
    }
}
