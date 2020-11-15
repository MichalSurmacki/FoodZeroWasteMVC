using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries
{
    public class GetSimilarProductsQuery : IRequest<List<string>>
    {
        public string SearchString { get; set; }
        public int Amount { get; set; }

        public GetSimilarProductsQuery(string searchString, int amount = 20)
        {
            SearchString = searchString;
            Amount = amount;
        }
    }

    public class GetSimilarProductsQueryHandler : IRequestHandler<GetSimilarProductsQuery, List<string>>
    {
        private readonly IApplicationDbContext _context;

        public GetSimilarProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<string>> Handle(GetSimilarProductsQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Ingredients
                .Select(i => i.Name)
                .Distinct()
                .ToList();

            var xd = result.OrderBy(i => StringsSimilarityAlgorithm.Calculate(i, request.SearchString))
                .Take(request.Amount)
                .ToList();

            return Task.FromResult(xd);
        }

        double StringSimilarityScore(string name, string searchString)
        {
            if (name.Contains(searchString))
            {
                return (double)searchString.Length / (double)name.Length;
            }

            return 0;
        }
    }
}
