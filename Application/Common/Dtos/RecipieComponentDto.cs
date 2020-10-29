using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.Common.Dtos
{
    public class RecipieComponentDto
    {
        public string SubTitle { get; set; }
        public IList<IngredientDto> Ingredients { get; set; }
    }
}