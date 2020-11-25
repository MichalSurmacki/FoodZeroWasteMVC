using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries
{
    public class GetProductsByUserQuery : IRequest<List<UserProductReadDto>>
    {
        public string UserName { get; set; }
        public GetProductsByUserQuery(string userName)
        {
            UserName = userName;
        }
    }

    public class GetProductsByUserQueryHandler : IRequestHandler<GetProductsByUserQuery, List<UserProductReadDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetProductsByUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<List<UserProductReadDto>> Handle(GetProductsByUserQuery request, CancellationToken cancellationToken)
        {
            var user = _context.UserData.FirstOrDefault(u => u.Email.Equals(request.UserName));

            var products = _context.UserProducts
                .Include(u => u.Product)
                    .ThenInclude(p => p.Tags)
                .Where(p => p.UserData.Id.Equals(user.Id))
                .OrderBy(p => p.ExpirationDate)
                .ToList();

            return Task.FromResult(_mapper.Map<List<UserProductReadDto>>(products));
        }
    }
}
