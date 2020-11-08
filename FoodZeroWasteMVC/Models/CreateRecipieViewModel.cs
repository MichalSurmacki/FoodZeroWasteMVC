using Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZeroWasteMVC.Models
{
    public class CreateRecipieViewModel
    {
        public CreateRecipieViewModel()
        {
            Components = new List<RecipieComponentDto>();
            InstructionSteps = new List<InstructionStepDto>();
        }

        public List<RecipieComponentDto> Components { get; set; }
        public List<InstructionStepDto> InstructionSteps { get; set; }
        public RecipieCreateDto Recipie { get; set; }
    }
}
