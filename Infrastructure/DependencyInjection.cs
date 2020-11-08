using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //dodanie connection stringa
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("FoodZeroWasteDb"));
            });

            //dependency injection dla interfejsu DbContext
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<AppDbContext>());

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));


            /*
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            */

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.Configure<IdentityOptions>(opt =>
            {
                //tutaj możemy zmienić opcje dotyczące hasła, konta itd.
                opt.Password.RequiredLength = 12;
                opt.Password.RequiredUniqueChars = 6;
            });




            return services;
        }
    }
}