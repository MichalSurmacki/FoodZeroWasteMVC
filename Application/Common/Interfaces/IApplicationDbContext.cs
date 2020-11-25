using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

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

        DbSet<UserData> UserData { get; set; }
        DbSet<UserProduct> UserProducts { get; set; }
        DbSet<FavouriteRecipie> FavouriteRecipies { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<UserSessionCaloriesLog> UserCaloriesLogs { get; set; }

        int SaveChanges();
    }
}