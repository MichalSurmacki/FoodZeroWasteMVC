using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Configuration
{
    public class UserDataConfiguration : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode();
        }
    }
}
