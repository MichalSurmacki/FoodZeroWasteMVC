using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public virtual IList<TagDto> Tags { get; set; }
    }
}
