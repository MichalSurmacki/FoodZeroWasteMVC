using Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZeroWasteMVC.Models.Products
{
    public class ProductsViewModel
    {
        public List<UserProductReadDto> Products { get; set; }
    }
}
