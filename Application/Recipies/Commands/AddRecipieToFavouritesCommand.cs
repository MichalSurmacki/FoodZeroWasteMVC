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

namespace Application.Recipies.Commands
{
    public class AddRecipieToFavouritesCommand : IRequest<RecipieDto>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public AddRecipieToFavouritesCommand(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public class AddRecipieToFavouritesHandler : IRequestHandler<AddRecipieToFavouritesCommand, RecipieDto>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;

            public AddRecipieToFavouritesHandler(IApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public Task<RecipieDto> Handle(AddRecipieToFavouritesCommand request, CancellationToken cancellationToken)
            {
                
                FavouriteRecipie favouriteRecipie = new FavouriteRecipie();

                var recipie = _context.Recipies.FirstOrDefault(r => r.Id.Equals(request.Id));
                favouriteRecipie.Recipie = recipie;

                var userData = _context.UserData.FirstOrDefault(u => u.Email.Equals(request.UserName));
                favouriteRecipie.UserData = userData;

                _context.FavouriteRecipies.Add(favouriteRecipie);
                _context.SaveChanges();

                //TODO co z tymi task.fromresult ??
                return Task.FromResult(_mapper.Map<RecipieDto>(recipie));
            }
        }
    }
}
