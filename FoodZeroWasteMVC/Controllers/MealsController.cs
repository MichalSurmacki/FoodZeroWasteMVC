using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Products.Queries;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Meal()
        {
            var productsQuery = new GetProductsByUserQuery(User.Identity.Name);
            List<ProductReadDto> productsResult = _mediator.Send(productsQuery).Result;



            return View();
        }

        public IActionResult Brekfast()
        {
            return View();
        }


    }
}
