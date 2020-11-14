using Application.Common.Mappings;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Common.Dtos
{
    public class RecipieCreateDto
    {
        public string Url { get; set; }
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
        public string CreatedBy { get; set; }
    }
}