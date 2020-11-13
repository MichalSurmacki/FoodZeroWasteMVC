using Application.Common;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Recipies.Queries
{
    public class GetRecipiesPaginationQuery : IRequest<PaginatedList<RecipieDto>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public GetRecipiesPaginationQuery(int pageNumber = 1, int pageSize = 10)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }

    public class GetRecipiesPaginationQueryHandler : IRequestHandler<GetRecipiesPaginationQuery, PaginatedList<RecipieDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRecipiesPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<RecipieDto>> Handle(GetRecipiesPaginationQuery request, CancellationToken cancellationToken)
        {
            var dd = _context.Recipies.Select(m => new RecipieDto
            {
                Id = m.Id,
                Title = m.Title,
                AllKcal = m.AllKcal,
                AllCarbs = m.AllCarbs
            });
            
            var a = await PaginatedList<RecipieDto>.CreateAsync(dd.AsNoTracking(), request.PageNumber, request.PageSize);

            return a;
        }
    }
}
