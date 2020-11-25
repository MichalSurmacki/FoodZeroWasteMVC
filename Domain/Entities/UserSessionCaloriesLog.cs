using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserSessionCaloriesLog
    {
        public Guid Id { get; set; }
        public float Kcal { get; set; }
        public DateTime Date { get; set; }
        public virtual UserData UserData { get; set; }
    }
}
