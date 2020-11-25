using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FavouriteRecipie
    {
        public Guid Id { get; set; }
        public virtual Recipie Recipie { get; set; }
        public virtual UserData UserData { get; set; }
    }
}
