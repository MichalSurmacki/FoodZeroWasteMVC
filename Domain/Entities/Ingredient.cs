using System;

namespace Domain.Entities
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public virtual Product Product { get; set; }
        public float Kcal { get; set; }
        public virtual RecipieComponent RecipieComponent { get; set; }
    }
}