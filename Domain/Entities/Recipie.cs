using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Recipie
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Servings { get; set; }
        public float AllKcal { get; set; }
        public float AllCarbs { get; set; }
        public float AllProtein { get; set; }
        public float AllFat { get; set; }
        public virtual IList<InstructionStep> InstructionSteps { get; set; }
        public virtual IList<Tag> Tags { get; set; }
        public virtual IList<RecipieComponent> Components { get; set; } 
        public virtual IList<Image> Images { get; set; }
        public string CreatedBy { get; set; }
        public virtual IList<FavouriteRecipie> FavouriteRecipies { get; set; }
    }
}