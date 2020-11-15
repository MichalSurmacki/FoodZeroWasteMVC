using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodZeroWasteMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            var query = new GetSimilarProductsQuery("Mąka");
            var result = _mediator.Send(query);


            return View();
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View()
        }

    }
}
