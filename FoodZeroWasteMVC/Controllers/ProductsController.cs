using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Account.Queries;
using Application.Common;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Products.Commands;
using Application.Products.Queries;
using FoodZeroWasteMVC.Models.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodZeroWasteMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;

        public ProductsController(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public IActionResult Index()
        {
            // TUTAJ TAGOWANIE PRODUKTÓW UZUNC KONTEXT Z PRODUCTS CONSTRUCTORA
            //TagProductsInDb b = new TagProductsInDb(_context);
            //b.TagProducts();

            var query = new GetProductsByUserQuery(User.Identity.Name);
            var result = _mediator.Send(query);

            ProductsViewModel model = new ProductsViewModel();
            model.Products = result.Result;

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            DetailsProductViewModel model = new DetailsProductViewModel();
            var query = new GetUserProductByIdQuery(id);
            var result = _mediator.Send(query);
            model.UserProduct = result.Result;
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateProductViewModel model = new CreateProductViewModel();
            model.ExpirationDate = DateTime.Today.Date;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel model)
        {
            var command = new CreateProductCommand(model.Product);
            var commandResult = _mediator.Send(command);

            var command2 = new AddProductToUserCommand(commandResult.Result, User.Identity.Name, model.ExpirationDate);
            var request2 = _mediator.Send(command2);

            return RedirectToAction("Details", new { request2.Result.Id });
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            DetailsProductViewModel model = new DetailsProductViewModel();
            var query = new GetUserProductByIdQuery(id);
            var result = _mediator.Send(query);
            model.UserProduct = result.Result;

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(DetailsProductViewModel model)
        {
            //var command = new UpdateProductCommand(model.UserProduct);
            //var result = _mediator.Send(command);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult NameAutoComplition(string prefix)
        {
            var query = new GetSimilarProductsQuery(prefix, 5);
            var result = _mediator.Send(query);

            return Json(result.Result);
        }
    }
}
