using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public virtual IList<FavouriteRecipie> FavouritesRecipies { get; set; }
        public virtual IList<UserProduct> UserProducts { get; set; }
        public virtual IList<UserSessionCaloriesLog> UserCaloriesLogs { get; set; }
    }
}