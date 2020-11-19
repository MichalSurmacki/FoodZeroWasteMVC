using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries
{
    public class GetProductsByUserQuery : IRequest<List<ProductReadDto>>
    {
        public string UserName { get; set; }
        public GetProductsByUserQuery(string userName)
        {
            UserName = userName;
        }
    }

    public class GetProductsByUserQueryHandler : IRequestHandler<GetProductsByUserQuery, List<ProductReadDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetProductsByUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<List<ProductReadDto>> Handle(GetProductsByUserQuery request, CancellationToken cancellationToken)
        {
            var user = _context.UserData.FirstOrDefault(u => u.Email.Equals(request.UserName));
            var products = _context.Products.Where(p => p.UserData.Id.Equals(user.Id)).ToList();

            return Task.FromResult(_mapper.Map<List<ProductReadDto>>(products));
        }
    }
}
