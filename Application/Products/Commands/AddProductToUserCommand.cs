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
    public class AddProductToUserCommand : IRequest<UserProductReadDto>
    {
        public ProductReadDto Product { get; set; }
        public string UserName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public AddProductToUserCommand(ProductReadDto product, string userName, DateTime expirationDate)
        {
            Product = product;
            UserName = userName;
            ExpirationDate = expirationDate;
        }
    }

    public class AddProductToUserCommandHandler : IRequestHandler<AddProductToUserCommand, UserProductReadDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public AddProductToUserCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<UserProductReadDto> Handle(AddProductToUserCommand request, CancellationToken cancellationToken)
        {
            var userData = _context.UserData.FirstOrDefault(u => u.Email.Equals(request.UserName));
            var product = _context.Products.FirstOrDefault(p => p.Id.Equals(request.Product.Id));

            UserProduct userProduct = new UserProduct()
            {
                UserData = userData,
                Product = product,
                ExpirationDate = request.ExpirationDate
            };

            _context.UserProducts.Add(userProduct);
            _context.SaveChanges();            

            return Task.FromResult(_mapper.Map<UserProductReadDto>(userProduct));
        }
    }
}
