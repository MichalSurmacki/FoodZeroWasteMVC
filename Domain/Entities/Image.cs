using System;

namespace Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public Recipie Recipie { get; set; }
    }
}