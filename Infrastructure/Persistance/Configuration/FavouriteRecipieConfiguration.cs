using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Configuration
{
    public class FavouriteRecipieConfiguration : IEntityTypeConfiguration<FavouriteRecipie>
    {
        public void Configure(EntityTypeBuilder<FavouriteRecipie> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
