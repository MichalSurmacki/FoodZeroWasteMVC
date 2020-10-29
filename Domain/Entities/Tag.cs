using System;

namespace Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Recipie Recipie { get; set; }
    }
}