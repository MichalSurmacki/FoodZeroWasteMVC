using System;

namespace Domain.Entities
{
    public class InstructionStep
    {
        public Guid Id { get; set; }
        public int StepOrder { get; set; }
        public string Step { get; set; }
        public virtual Recipie Recipie { get; set; }
    }
}