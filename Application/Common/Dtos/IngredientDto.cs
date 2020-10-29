using System;

namespace Application.Common.Dtos
{
    public class IngredientDto
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public float Kcal { get; set; }
    }
}