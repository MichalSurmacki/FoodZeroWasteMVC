using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class RecipieComponentConfiguration : IEntityTypeConfiguration<RecipieComponent>
    {
        public void Configure(EntityTypeBuilder<RecipieComponent> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(r => r.SubTitle)
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode();
        }
    }
}