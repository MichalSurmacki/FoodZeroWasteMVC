using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public int Weight { get; set; }
        [PersonalData]
        public int Age { get; set; }
        [PersonalData]
        public int Height { get; set; }
        [PersonalData]
        public Gender gender { get; set; }
    }
}
