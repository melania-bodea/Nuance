using Microsoft.AspNetCore.Identity;

namespace Nuance.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Wardrobe? Wardrobe { get; set; }
    }
}
