using System;

namespace Application.Common.Dtos
{
    public class IngredientDto
    {
        public virtual ProductReadDto Product { get; set; }
        public float Kcal { get; set; }
    }
}