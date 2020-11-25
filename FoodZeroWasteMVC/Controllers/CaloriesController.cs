using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZeroWasteMVC.Controllers
{
    public class CaloriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Counter()
        {
            return View();
        }
    }
}
