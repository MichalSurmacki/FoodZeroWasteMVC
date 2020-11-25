using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
    }
}
