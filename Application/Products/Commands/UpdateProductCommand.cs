using Application.Common.Dtos;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public ProductReadDto Product { get; set; }
        public UpdateProductCommand(ProductReadDto product)
        {
            Product = product;
        }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id.Equals(request.Product.Id));

            product.Name = request.Product.Name;
            product.ExpirationDate = request.Product.ExpirationDate;
            product.Amount = request.Product.Amount;
            product.Unit = request.Product.Unit;

            _context.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
