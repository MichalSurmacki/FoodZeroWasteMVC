using System;

namespace Domain.Entities
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public float Kcal { get; set; }
        public RecipieComponent RecipieComponent { get; set; }
    }
}