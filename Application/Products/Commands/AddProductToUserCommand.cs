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

namespace Application.Products.Commands
{
    public class AddProductToUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public ProductReadDto Product { get; set; }
        public AddProductToUserCommand(string name, ProductReadDto product)
        {
            Name = name;
            Product = product;
        }
    }

    public class AddProductToUserCommandHandler : IRequestHandler<AddProductToUserCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public AddProductToUserCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<bool> Handle(AddProductToUserCommand request, CancellationToken cancellationToken)
        {
            var userData = _context.UserData.FirstOrDefault(u => u.Email.Equals(request.Name));

            var product = _context.Products.FirstOrDefault(p => p.Id.Equals(request.Product.Id));
            product.UserData = userData;

            _context.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
