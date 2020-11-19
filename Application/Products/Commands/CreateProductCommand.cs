using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands
{
    public class CreateProductCommand : IRequest<ProductReadDto>
    {
        public ProductCreateDto Product { get; set; }
        public CreateProductCommand(ProductCreateDto product)
        {
            Product = product;
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductReadDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<ProductReadDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.Product);
            _context.Products.Add(product);
            _context.SaveChanges();

            return Task.FromResult(_mapper.Map<ProductReadDto>(product));
        }
    }
}
