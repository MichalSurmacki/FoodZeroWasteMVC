using Application.Common.Dtos;
using Application.Recipies.Commands;
using Application.Recipies.Queries;
using FoodZeroWasteMVC.Models;
using FoodZeroWasteMVC.Models.Recipies;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodZeroWasteMVC.Controllers
{
    public class RecipiesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipiesController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        // GET lista wszystkich przepisów - recipie
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {

            var query = new GetUserFavouriteRecipiesQuery(User.Identity.Name);
            var result = _mediator.Send(query).Result;

            var query2 = new GetRecipiesPaginationQuery(pageNumber, pageSize);
            var result2 = _mediator.Send(query2).Result;

            var model = new RecipiesViewModel();
            model.FavouriteRecipies = result;
            model.Recipies = result2;

            return View(model);
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
            //osobne propertki bo jak get to zeby nie wyswietlalo na stronce zer
            model.Recipie.Components = model.Components;
            model.Recipie.InstructionSteps = model.InstructionSteps;
            
            model.Recipie.CreatedBy = User.Identity.Name;

            var command = new CreateRecipieCommand(model.Recipie);
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
        public IActionResult UpdateInstructionsSteps(List<InstructionStepDto> model)
        {
            return PartialView("_InstructionsListPartial", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult UpdateIngredients(List<IngredientDto> model)
        {
            return PartialView("_IngredientsListPartial", model);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult UpdateComponents(List<RecipieComponentDto> model)
        {
            return PartialView("_ComponentsListPartial", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddToFavourites(Guid id)
        {
            var command = new AddRecipieToFavouritesCommand(id, User.Identity.Name);
            var result = _mediator.Send(command);

            return PartialView("_RemoveFavouriteRecipiePartial", id);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult RemoveFromFavourites(Guid id)
        {
            var command = new RemoveRecipieFromFavouritesCommand(id, User.Identity.Name);
            var result = _mediator.Send(command);

            return PartialView("_FavouriteRecipiePartial", id);
        }
    }
}