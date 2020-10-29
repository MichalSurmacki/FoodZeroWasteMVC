using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.Common.Dtos
{
    public class RecipieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Servings { get; set; }
        public float AllKcal { get; set; }
        public float AllCarbs { get; set; }
        public float AllProtein { get; set; }
        public float AllFat { get; set; }
        public IList<InstructionStepDto> InstructionSteps { get; set; }
        public IList<TagDto> Tags { get; set; }
        public IList<RecipieComponentDto> Components { get; set; } 
        public IList<ImageDto> Images { get; set; }
    }
}