using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodZeroWasteMVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailUsed", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password",
            ErrorMessage = "Hasła nie są zgodne")]
        public string ConfirmPassword { get; set; }
        
        public int Weight { get; set; }
        
        public int Age { get; set; }
        
        public int Height { get; set; }
        
        public Gender Gender { get; set; }
    }
}
