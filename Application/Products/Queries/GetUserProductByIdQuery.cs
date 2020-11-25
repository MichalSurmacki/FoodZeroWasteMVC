using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries
{
    public class GetUserProductByIdQuery : IRequest<UserProductReadDto>
    {
        public Guid Id { get; set; }
        public GetUserProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetUserProductByIdQuery, UserProductReadDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public Task<UserProductReadDto> Handle(GetUserProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _context.UserProducts
                .Include(u => u.Product)
                    .ThenInclude(p => p.Tags)
                .Include(u => u.UserData)
                    .ThenInclude(u => u.FavouritesRecipies)
                .Where(p => p.Id.Equals(request.Id)).FirstOrDefault();
            
            return Task.FromResult(_mapper.Map<UserProductReadDto>(product));
        }
    }
}
