using Microsoft.AspNetCore.Identity;

namespace Avion.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool RememberMe { get; set; }
    }
}
