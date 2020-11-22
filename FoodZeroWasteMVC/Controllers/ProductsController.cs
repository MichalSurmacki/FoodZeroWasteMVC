using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos;
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

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
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
            var query = new GetProductByIdQuery(id);
            var result = _mediator.Send(query);
            model.Product = result.Result;
            
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
            model.Product.ExpirationDate = model.ExpirationDate;

            var command = new CreateProductCommand(model.Product);
            var result = _mediator.Send(command);

            var command2 = new AddProductToUserCommand(User.Identity.Name, result.Result);
            var request2 = _mediator.Send(command2);

            return RedirectToAction("Details", new { id = request2.Id });
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            DetailsProductViewModel model = new DetailsProductViewModel();
            var query = new GetProductByIdQuery(id);
            var result = _mediator.Send(query);
            model.Product = result.Result;

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(DetailsProductViewModel model)
        {
            var command = new UpdateProductCommand(model.Product);
            var result = _mediator.Send(command);
            
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
