using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class MealRecommendationReadDto
    {
        public RecipieReadDto Recipie { get; set; }
        public UserProductReadDto Products { get; set; }

    }
}
