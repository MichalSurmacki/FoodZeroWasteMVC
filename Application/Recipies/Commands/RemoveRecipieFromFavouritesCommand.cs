using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Recipies.Commands
{
    public class RemoveRecipieFromFavouritesCommand : IRequest<bool>
    {
        public string UserEmail { get; set; }
        public Guid RecipieId { get; set; }

        public RemoveRecipieFromFavouritesCommand(Guid recipieId, string userEmail)
        {
            UserEmail = userEmail;
            RecipieId = recipieId;
        }
    }

    public class RemoveRecipieFromFavouritesCommandHandler : IRequestHandler<RemoveRecipieFromFavouritesCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public RemoveRecipieFromFavouritesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(RemoveRecipieFromFavouritesCommand request, CancellationToken cancellationToken)
        {
            var favouriteRecipie = _context.FavouriteRecipies
                .FirstOrDefault(f => f.UserData.Email.Equals(request.UserEmail) && (f.Recipie.Id.Equals(request.RecipieId)));            
            
            if(favouriteRecipie != null)
            {
                _context.FavouriteRecipies.Remove(favouriteRecipie);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
