using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodZeroWaste.Application.Recipies.Queries
{
    public class GetAllRecipiesQuery : IRequest<List<RecipieDto>>
    {
        
    }

    public class GetAllRecipiesQueryHandler : IRequestHandler<GetAllRecipiesQuery, List<RecipieDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllRecipiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RecipieDto>> Handle(GetAllRecipiesQuery request, CancellationToken cancellationToken)
        { 
            var allRecipies =  _context.Recipies.ToList();
            var allTags =  _context.Tags.ToList();
            var allImages =  _context.Images.ToList();
            var allIngredients = _context.Ingredients.ToList();
            var allInstructionSteps = _context.InstructionsSteps.ToList();
            var allRecipiesComponents = _context.RecipieComponents.ToList();

            foreach(Recipie recipie in allRecipies)
            {
                recipie.Tags = allTags.Where(t => t.Recipie.Id.Equals(recipie.Id)).ToList();
                recipie.Images = allImages.Where(i => i.Recipie.Id.Equals(recipie.Id)).ToList();
                recipie.InstructionSteps = allInstructionSteps.Where(i => i.Recipie.Id.Equals(recipie.Id)).ToList(); 
                var recipieComponents = allRecipiesComponents.Where(r => r.Recipie.Id.Equals(recipie.Id)).ToList();
                foreach(RecipieComponent component in recipieComponents)
                {
                    component.Ingredients = allIngredients.Where(i => i.RecipieComponent.Id.Equals(component.Id)).ToList();
                }
                recipie.Components = recipieComponents;
            }
            return await Task.FromResult(_mapper.Map<List<RecipieDto>>(allRecipies));
        }
    }
}