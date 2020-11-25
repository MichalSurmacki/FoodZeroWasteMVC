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

namespace Application.Recipies.Queries
{
    public class GetRecipieByIdQuery : IRequest<RecipieReadDto>
    {
        public Guid RecipieId { get; set; }
        public GetRecipieByIdQuery(Guid recipieId)
        {
            RecipieId = recipieId;
        }

        public class GetRecipieByIdQueryHandler : IRequestHandler<GetRecipieByIdQuery, RecipieReadDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetRecipieByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<RecipieReadDto> Handle(GetRecipieByIdQuery request, CancellationToken cancellationToken)
            {
                var recipie = _context.Recipies
                    .Include(r => r.InstructionSteps)
                    .Include(r => r.Tags)
                    .Include(r => r.Images)
                    .Include(r => r.Components)
                        .ThenInclude(c => c.Ingredients)
                            .ThenInclude(i => i.Product)
                    
                    .FirstOrDefault(r => r.Id.Equals(request.RecipieId));

                recipie.InstructionSteps = recipie.InstructionSteps.OrderBy(i => i.StepOrder).ToList();

                return await Task.FromResult(_mapper.Map<RecipieReadDto>(recipie));
            }
        }
    }
}