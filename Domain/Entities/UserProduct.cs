using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserProduct
    {
        public Guid Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual Product Product { get; set; }
        public virtual UserData UserData { get; set; }
    }
}