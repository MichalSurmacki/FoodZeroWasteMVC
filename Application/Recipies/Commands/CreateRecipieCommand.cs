using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Common.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Recipies.Commands
{
    public class CreateRecipieCommand : IRequest<Recipie>
    {
        public RecipieCreateDto Recipie { get; set; }
        public CreateRecipieCommand(RecipieCreateDto recipie)
        {
            Recipie = recipie;
        }
    }
    public class CreateRecipieCommandHandler : IRequestHandler<CreateRecipieCommand, Recipie>
    {
        private readonly IMapper _mapper;

        public CreateRecipieCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<Recipie> Handle(CreateRecipieCommand request, CancellationToken cancellationToken)
        {
            var recipie = _mapper.Map<Recipie>(request.Recipie);
            throw new System.NotImplementedException();
        }
    }
}