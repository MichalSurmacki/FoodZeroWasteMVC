﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public ICollection<FavouriteRecipie> FavouritesRecipies { get; set; }
    }
}