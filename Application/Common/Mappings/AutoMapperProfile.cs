using AutoMapper;
using Application.Common.Dtos;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Recipie, RecipieDto>();

            CreateMap<RecipieCreateDto, Recipie>();

            CreateMap<RecipieCreateDto, RecipieDto>();

            CreateMap<Tag, TagDto>();
            CreateMap<RecipieComponent, RecipieComponentDto>();
            CreateMap<Image, ImageDto>();
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<InstructionStep, InstructionStepDto>();
        }
    }
}