using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class InstructionStepConfiguration : IEntityTypeConfiguration<InstructionStep>
    {
        public void Configure(EntityTypeBuilder<InstructionStep> builder)
        {   
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(i => i.Step)
                .HasMaxLength(2500)
                .IsRequired()
                .IsUnicode();

            builder.Property(i => i.StepOrder)
                .IsRequired();
        }
    }
}