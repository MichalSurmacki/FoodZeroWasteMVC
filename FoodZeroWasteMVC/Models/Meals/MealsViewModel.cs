using Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZeroWasteMVC.Models.Meals
{
    public class MealsViewModel
    {
        public List<MealRecommendationReadDto> Recommendations { get; set; }
    }
}
