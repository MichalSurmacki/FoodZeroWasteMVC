using System.Reflection;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());


            //var mapperConfiguration = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(new AutoMapperProfile());
            //});


            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}