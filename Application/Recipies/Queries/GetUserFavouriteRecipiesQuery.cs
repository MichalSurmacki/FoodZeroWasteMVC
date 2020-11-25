using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Recipies.Queries
{
    public class GetUserFavouriteRecipiesQuery : IRequest<List<RecipieReadDto>>
    {
        public string UserName { get; set; }
        public GetUserFavouriteRecipiesQuery(string userName)
        {
            UserName = userName;
        }
    }

    public class GetUserFavouriteRecipiesQueryHandle : IRequestHandler<GetUserFavouriteRecipiesQuery, List<RecipieReadDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetUserFavouriteRecipiesQueryHandle(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<RecipieReadDto>> Handle(GetUserFavouriteRecipiesQuery request, CancellationToken cancellationToken)
        {
            var fav = _context.FavouriteRecipies
                .Include(x => x.Recipie)
                .Where(f => f.UserData.Email.Equals(request.UserName)).ToList();

            var ans = fav.Select(f => f.Recipie).ToList();

            return await Task.FromResult(_mapper.Map<List<RecipieReadDto>>(ans));
        }
    }
}
