using AutoMapper;
using Application.Common.Dtos;
using Domain.Entities;
using System.Reflection;
using System;
using System.Linq;

namespace Application.Common.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

           
            CreateMap<Recipie, RecipieReadDto>();

            CreateMap<InstructionStepDto, InstructionStep>();
            CreateMap<TagDto, Tag>();
            CreateMap<RecipieComponentDto, RecipieComponent>();
            CreateMap<ImageDto, Image>();
            CreateMap<RecipieCreateDto, Recipie>();
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<Recipie, RecipieCreateDto>();

            CreateMap<RecipieCreateDto, RecipieReadDto>();

            CreateMap<Tag, TagDto>();
            CreateMap<RecipieComponent, RecipieComponentDto>();
            CreateMap<Image, ImageDto>();
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<InstructionStep, InstructionStepDto>();

            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();

            CreateMap<UserProduct, UserProductReadDto>();

            CreateMap<UserData, UserDataReadDto>();

            CreateMap<UserSessionCaloriesLog, UserSessionCaloriesLogReadDto>();
            CreateMap<UserSessionCaloriesLogCreateDto, UserSessionCaloriesLog>();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}