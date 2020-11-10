using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodZeroWasteMVC.Controllers
{
    public class FavouriteRecipiesController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavouriteRecipiesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //TODO
            //Tutaj wyjęcie z bazy wszystkich ulubionych przepisów użytkownika
            return View();
        }

        [HttpPost]
        public IActionResult Create()
        {
            //Nie zwracanie widoku tylko redirect
            return View();
        }

        [HttpPatch]
        public IActionResult Update()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return View();
        }
    }
}