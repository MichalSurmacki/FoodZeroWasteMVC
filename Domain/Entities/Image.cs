using System;

namespace Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public virtual Recipie Recipie { get; set; }
    }
}