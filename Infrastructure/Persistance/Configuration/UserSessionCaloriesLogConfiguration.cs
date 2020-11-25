using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Configuration
{
    public class UserSessionCaloriesLogConfiguration : IEntityTypeConfiguration<UserSessionCaloriesLog>
    {
        public void Configure(EntityTypeBuilder<UserSessionCaloriesLog> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
