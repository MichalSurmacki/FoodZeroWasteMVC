using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(t => t.Value)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();
        }
    }
}