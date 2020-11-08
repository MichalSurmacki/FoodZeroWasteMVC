using Application.Common.Dtos;
using Application.Recipies.Commands;
using FoodZeroWaste.Application.Recipies.Queries;
using FoodZeroWasteMVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FoodZeroWasteMVC.Controllers
{
    public class RecipiesController : Controller
    {
        private readonly IMediator _mediator;

        public RecipiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET lista wszystkich przepisów - recipie
        public IActionResult Index()
        {
            var query = new GetAllRecipiesQuery();
            var result = _mediator.Send(query).Result;
            return View(result);
        }

        // GET recipie o danym id
        public IActionResult Details(Guid id)
        {
            var query = new GetRecipieByIdQuery(id);
            var result = _mediator.Send(query).Result;
            return View(result);
        }

        // GET forme do stworzenia nowego przepisu
        // Displays a form for creating a new resource. For example, displays a form for creating a new product.
        [HttpGet]
        public IActionResult Create()
        {
            CreateRecipieViewModel model = new CreateRecipieViewModel();
            return View(model);
        }

        // POST nowy przepis - recipie
        // Inserts a new resource into the database. Typically, you redirect to another action after performing an Insert.
        [HttpPost]
        public IActionResult Create(CreateRecipieViewModel model)
        {
            model.Recipie.Components = model.Components;

            RecipieCreateDto recipie = new RecipieCreateDto();

            var command = new CreateRecipieCommand(recipie);
            var result = _mediator.Send(command).Result;

            return RedirectToAction("Details", new { id = result.Id });
        }

        // Displays a form for editing an existing resource.For example, displays a form for editing a product with an Id of 5.
        public IActionResult Edit(Guid id)
        {
            return View();
        }

        // Updates existing resources in the database. Typically, you redirect to another action after performing an Update.
        public IActionResult Update()
        {
            return View();
        }

        // Displays a page that confirms whether or not you want to delete a resource from the database.
        public IActionResult Destroy()
        {
            return View();
        }

        // Deletes a resource from the database. Typically, you redirect to another action after performing a Delete.
        public IActionResult Delete()
        {
            return View();
        }





        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult UpdateViewData(List<IngredientDto> model)
        {
            return PartialView("_DisplayListIngredientsDetails", model);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult UpdateComponents(List<RecipieComponentDto> model)
        {
            return PartialView("_DisplayListComponentsDetails", model);
        }
    }
}