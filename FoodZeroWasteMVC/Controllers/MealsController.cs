using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Meals.Queries;
using Application.Products.Queries;
using FoodZeroWasteMVC.Models.Meals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodZeroWasteMVC.Controllers
{
    public class MealsController : Controller
    {
        private readonly IMediator _mediator;

        public MealsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //GET all products of user
            var productsQuery = new GetProductsByUserQuery(User.Identity.Name);
            var productsResult = _mediator.Send(productsQuery).Result;


            MealsViewModel model = new MealsViewModel();
            var query = new GetMealsRecommendationsQuery(productsResult);
            var result = _mediator.Send(query).Result;
            model.Recommendations = result;

            return View(model);
        }

        [HttpGet]
        public IActionResult Meal()
        {
           
            return View();
        }
    }
}
