using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FavouriteRecipie
    {
        public Guid Id { get; set; }
        public Recipie Recipie { get; set; }
        public UserData UserData { get; set; }
    }
}
