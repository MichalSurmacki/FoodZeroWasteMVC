using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.Commands
{
    public class AddUserDataToAppDbCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public AddUserDataToAppDbCommand(string userName)
        {
            UserName = userName;
        }
    }

    public class AddUserDataToAppDbCommandHandler : IRequestHandler<AddUserDataToAppDbCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public AddUserDataToAppDbCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        //TODO co z tym typem zwracanym ??
        public Task<bool> Handle(AddUserDataToAppDbCommand request, CancellationToken cancellationToken)
        {
            _context.UserData.Add(new UserData { Email = request.UserName });
            _context.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
