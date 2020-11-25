using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class RecipieComponent
    {
        public Guid Id { get; set; }
        public string SubTitle { get; set; }
        public virtual IList<Ingredient> Ingredients { get; set; }
        public virtual Recipie Recipie { get; set; }
    }
}