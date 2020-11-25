using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class UserSessionCaloriesLogCreateDto
    {
        public float Kcal { get; set; }
        public DateTime Date { get; set; }
        public virtual UserData UserData { get; set; }
    }
}
