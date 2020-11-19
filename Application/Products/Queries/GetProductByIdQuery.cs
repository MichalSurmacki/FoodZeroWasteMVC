using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductReadDto>
    {
        public Guid Id { get; set; }
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductReadDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public Task<ProductReadDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id.Equals(request.Id));

            return Task.FromResult(_mapper.Map<ProductReadDto>(product));
        }
    }
}
