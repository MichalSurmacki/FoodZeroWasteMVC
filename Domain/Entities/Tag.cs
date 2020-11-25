using System;

namespace Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public virtual Recipie Recipie { get; set; }
        public virtual Product Product { get; set; }
    }
}