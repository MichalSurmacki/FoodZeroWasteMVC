using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

            builder.Property(p => p.Unit)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(p => p.Amount)
                .IsRequired();
        }
    }
}
