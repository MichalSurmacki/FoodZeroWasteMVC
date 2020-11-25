using Application.Common.Dtos;
using Application.Common.EqualityComparers;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Meals.Queries
{
    public class GetMealsRecommendationsQuery : IRequest<List<MealRecommendationReadDto>>
    {
        public List<UserProductReadDto> UserProducts { get; private set; }
        public int Amount { get; set; }
        public GetMealsRecommendationsQuery(List<UserProductReadDto> userProducts, int amount = 3)
        {
            UserProducts = userProducts;
            Amount = amount;
        }
    }

    public class GetMealRecommendationQueryHandler : IRequestHandler<GetMealsRecommendationsQuery, List<MealRecommendationReadDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMealRecommendationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<MealRecommendationReadDto>> Handle(GetMealsRecommendationsQuery request, CancellationToken cancellationToken)
        {
            List<MealRecommendationReadDto> recommendation = new List<MealRecommendationReadDto>();

            var today = DateTime.Now;

            //produkty które zostały przeterminowane
            var blackProducts = request.UserProducts
                .Where(u => u.ExpirationDate < today)
                .ToList();

            //produkty ze zbliżającym się terminem ważności (7 dni)
            var redProducts = GetUserProductsFromToDate(request.UserProducts, today, today.AddDays(7));

            //składniki wykorzystywane w przepisach które posiadają takie same tagi jak produkty użytkownika
            var ingredientsWithProductsTags = GetIngredientsWithSameTagsAsUserProducts(redProducts);

            var recipiesWithIngredients = GetRecipiesContainingIngredients(ingredientsWithProductsTags);

            //List<Recipie> allRecipies = new List<Recipie>();
            //List<Ingredient> allIngredients = new List<Ingredient>();

            //foreach (UserProductReadDto p in redProducts)
            //{
            //    var tags = p.Product.Tags.Select(t => t.Value).ToList();

            //    var ingredients = _context.Ingredients
            //        .Include(i => i.Product)
            //            .ThenInclude(p => p.Tags)
            //        .Where(i => i.Product.Tags.Any(i => tags.Contains(i.Value)))
            //        .ToList();

            //    var components = _context.RecipieComponents
            //        .Where(c => c.Ingredients.Any(i => ingredients.Contains(i)))
            //        .ToList();

            //    var recipies = _context.Recipies
            //        .Where(r => r.Components.Any(c => components.Contains(c)))
            //        .ToList();

            //    allRecipies.AddRange(recipies);
            //    allIngredients.AddRange(ingredients);
            //}

            //allRecipies = allRecipies.Distinct().Take(request.Amount).ToList();
            //allIngredients = allIngredients.Distinct().Take(request.Amount).ToList();

            int bestIngredientCount = 0;
            List<Recipie> recommendedRecipies = new List<Recipie>();

            foreach (Recipie r in recipiesWithIngredients)
            {
                int count = 0;
                r.Components.ToList()
                    .ForEach(c => count = (c.Ingredients.Intersect(ingredientsWithProductsTags).ToList().Count));
                if (count > bestIngredientCount)
                {
                    bestIngredientCount = count;
                    recommendedRecipies = new List<Recipie>();
                    recommendedRecipies.Add(r);
                }
                else if (count == bestIngredientCount)
                {
                    recommendedRecipies.Add(r);
                }
            }

            foreach (Recipie r in recommendedRecipies)
            {
                recommendation.Add(new MealRecommendationReadDto { Recipie = _mapper.Map<RecipieReadDto>(r) });
            }




            //produkty ze zbliżającym się terminem ważności (14 dni) 
            var yellowProducts = request.UserProducts
                .Where(u => (u.ExpirationDate < today.AddDays(14)) && (u.ExpirationDate >= today.AddDays(7)))
                .ToList();

            //produkty z terminem ważności większym niż 14 dni 
            var greenProducts = request.UserProducts
                .Where(u => u.ExpirationDate > today.AddDays(14))
                .ToList();


            return Task.FromResult(recommendation);
        }

        public List<UserProductReadDto> GetUserProductsFromToDate(List<UserProductReadDto> userProductsList, DateTime dateFrom, DateTime dateTo)
        {
            var products = userProductsList
                .Where(u => (u.ExpirationDate >= dateFrom && u.ExpirationDate <= dateTo))
                .ToList();

            return products;
        }

        public List<Ingredient> GetIngredientsWithSameTagsAsUserProducts(List<UserProductReadDto> userProductsList)
        {
            List<Ingredient> allIngredients = new List<Ingredient>();

            foreach (UserProductReadDto p in userProductsList)
            {
                var tags = p.Product.Tags.Select(t => t.Value).ToList();

                var ingredients = _context.Ingredients
                    .Include(i => i.Product)
                        .ThenInclude(p => p.Tags)
                    .Where(i => i.Product.Tags.Any(i => tags.Contains(i.Value)))
                    .ToList();

                allIngredients.AddRange(ingredients);
            }

            allIngredients = allIngredients.Distinct().ToList();
            return allIngredients;
        }

        public List<Recipie> GetRecipiesContainingIngredients(List<Ingredient> ingredientsList)
        {
            List<Recipie> allRecipies = new List<Recipie>();

            var components = _context.RecipieComponents
                    .Where(c => c.Ingredients.Any(i => ingredientsList.Contains(i)))
                    .ToList();

            var recipies = _context.Recipies
                .Where(r => r.Components.Any(c => components.Contains(c)))
                .ToList();

            allRecipies.AddRange(recipies);
            allRecipies = allRecipies.Distinct().ToList();
            
            return allRecipies;
        }
    }
}
