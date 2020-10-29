using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Recipie> Recipies { get; set; }
        DbSet<InstructionStep> InstructionsSteps { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<RecipieComponent> RecipieComponents { get; set; }
        DbSet<Tag> Tags { get; set; }
    }
}