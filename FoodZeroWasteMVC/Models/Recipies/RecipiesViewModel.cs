using Application.Common;
using Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZeroWasteMVC.Models.Recipies
{
    public class RecipiesViewModel
    {
        public List<RecipieDto> FavouriteRecipies { get; set; }

        public PaginatedList<RecipieDto> Recipies { get; set; }
    }
}
