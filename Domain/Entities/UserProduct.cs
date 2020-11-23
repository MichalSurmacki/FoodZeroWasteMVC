using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserProduct
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public DateTime ExpirationDate { get; set; }
        public UserData UserData { get; set; }
    }
}