using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            request.SearchString = request.SearchString.ToLower();
            request.SearchString = request.SearchString.Trim();
            var searchStrings = request.SearchString.Split(" ");

            if(searchStrings.Length <= 1)
            {
                var tags = _context.Tags
                    .Include(t => t.Product)
                    .Where(t => (t.Product != null) && (t.Value.Contains(searchStrings[0])))
                    .ToList();

                var list = tags.Select(t => t.Product.Name).ToList();
                list = list.Distinct().Take(request.Amount).ToList();

                return Task.FromResult(list);
            }

            List<List<Tag>> tagsLists = new List<List<Tag>>();

            //Wybieranie z bazy tagów
            foreach (string s in searchStrings)
            {
                tagsLists.Add(_context.Tags
                .Include(t => t.Product)
                .Where(t => (t.Product != null) && (t.Value.Contains(s)))
                .ToList());
            }

            //List<Product> commonProducts = new List<Product>();

            var commonProducts = tagsLists[0].Select(t => t.Product).Intersect(tagsLists[1].Select(t => t.Product)).ToList();

            //Nie ma produktu/ów o tych samych tagach
            if(commonProducts.Count == 0)
            {
                var list = tagsLists[0].Select(t => t.Product.Name).ToList();
                list = list.Distinct().Take(request.Amount).ToList();
                return Task.FromResult(list);
            }

            //Jeśli lista jest długości 2 i commonProducts > 0
            if (searchStrings.Length == 2)
            {
                var list = commonProducts.Select(t => t.Name).ToList();
                list = list.Distinct().Take(request.Amount).ToList();
                return Task.FromResult(list);
            }
            //Jeśli lista jest większa niż 2
            else
            {

                List<Product> mostCommonList = commonProducts.ToList();

                //Wyszukieanie wspólnych tagów
                for (int i = 2; i < tagsLists.Count; i++)
                {

                    commonProducts = commonProducts.Intersect(tagsLists[i].Select(t => t.Product)).ToList();
                    if(commonProducts.Count == 0)
                    {
                        commonProducts = mostCommonList.ToList();
                    }
                    else
                    {
                        mostCommonList = commonProducts.ToList();
                    }
                }
                var list = commonProducts.Select(t => t.Name).ToList();
                list = list.Distinct().Take(request.Amount).ToList();
                return Task.FromResult(list);
            }
        }
    }
}
