using System;
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
    public class GetRecipieByIdQuery : IRequest<RecipieDto>
    {
        public Guid RecipieId { get; set; }
        public GetRecipieByIdQuery(Guid recipieId)
        {
            RecipieId = recipieId;
        }

        public class GetRecipieByIdQueryHandler : IRequestHandler<GetRecipieByIdQuery, RecipieDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetRecipieByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<RecipieDto> Handle(GetRecipieByIdQuery request, CancellationToken cancellationToken)
            {
                var recipie = _context.Recipies.FirstOrDefault(r => r.Id.Equals(request.RecipieId));
                recipie.InstructionSteps = _context.InstructionsSteps.Where(i => i.Recipie.Id.Equals(request.RecipieId)).ToList();
                recipie.Tags = _context.Tags.Where(t => t.Recipie.Id.Equals(request.RecipieId)).ToList();
                recipie.Images = _context.Images.Where(i => i.Recipie.Id.Equals(request.RecipieId)).ToList();
                recipie.Components = _context.RecipieComponents.Where(r => r.Recipie.Id.Equals(request.RecipieId)).ToList();
                foreach(RecipieComponent component in recipie.Components)
                {
                    component.Ingredients = _context.Ingredients.Where(i => i.RecipieComponent.Id.Equals(component.Id)).ToList();
                }

                return await Task.FromResult(_mapper.Map<RecipieDto>(recipie));
            }
        }
    }
}