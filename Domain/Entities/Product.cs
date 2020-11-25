using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public virtual IList<Tag> Tags { get; set; }
    }
}
