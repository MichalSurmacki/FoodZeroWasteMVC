using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class RecipieComponent
    {
        public Guid Id { get; set; }
        public string SubTitle { get; set; }
        public IList<Ingredient> Ingredients { get; set; }
        public Recipie Recipie { get; set; }
    }
}