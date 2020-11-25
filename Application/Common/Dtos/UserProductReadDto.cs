using Domain.Entities;
using System;

namespace Application.Common.Dtos
{
    public class UserProductReadDto
    {
        public Guid Id { get; set; }
        public ProductReadDto Product { get; set; }
        public DateTime ExpirationDate { get; set; }
        public UserDataReadDto UserData { get; set; }
    }
}
