using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Common.Dtos;
using Domain.Entities;
using MediatR;
using Application.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Recipies.Commands
{
    public class CreateRecipieCommand : IRequest<RecipieDto>
    {
        public RecipieCreateDto Recipie { get; set; }
        public CreateRecipieCommand(RecipieCreateDto recipie)
        {
            Recipie = recipie;
        }
    }

    public class CreateRecipieCommandHandler : IRequestHandler<CreateRecipieCommand, RecipieDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateRecipieCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<RecipieDto> Handle(CreateRecipieCommand request, CancellationToken cancellationToken)
        {
            var recipie = _mapper.Map<Recipie>(request.Recipie);
            _context.Recipies.Add(recipie);
            _context.SaveChanges();
            
            return Task.FromResult(_mapper.Map<RecipieDto>(recipie));
        }
    }
}