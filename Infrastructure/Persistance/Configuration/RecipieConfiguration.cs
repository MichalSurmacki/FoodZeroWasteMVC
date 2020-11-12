using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Configuration
{
    public class RecipieConfiguration : IEntityTypeConfiguration<Recipie>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Recipie> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(r => r.Url)
                .HasMaxLength(250);

            builder.Property(r => r.Title)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            builder.Property(r => r.Servings)
                .IsRequired();

            builder.Property(r => r.AllCarbs)
                .IsRequired();

            builder.Property(r => r.AllFat)
                .IsRequired();

            builder.Property(r => r.AllKcal)
                .IsRequired();

            builder.Property(r => r.AllProtein)
                .IsRequired();

            builder.Property(r => r.CreatedBy)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}