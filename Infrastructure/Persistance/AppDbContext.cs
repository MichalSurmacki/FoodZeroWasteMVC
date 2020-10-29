using System.Reflection;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Recipie> Recipies { get; set; }
        public DbSet<InstructionStep> InstructionsSteps { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipieComponent> RecipieComponents { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Recipie>()
                .HasMany(r => r.Images)
                .WithOne(i => i.Recipie);

            builder.Entity<Recipie>()
                .HasMany(r => r.InstructionSteps)
                .WithOne(i => i.Recipie);

            builder.Entity<Recipie>()
                .HasMany(r => r.Components)
                .WithOne(c => c.Recipie);

            builder.Entity<RecipieComponent>()
                .HasMany(r => r.Ingredients)
                .WithOne(i => i.RecipieComponent);

            builder.Entity<Recipie>()
                .HasMany(r => r.Tags)
                .WithOne(t => t.Recipie);

            //te 2 linijki wystarczyły żeby w Persistance/Configuration umiescic to FluentAPI i "zczytało?" z tamtąd jak ma wyglądać ogranienie??? (???)
            //chyba nie ten interfejs sam jakos to robi??
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}