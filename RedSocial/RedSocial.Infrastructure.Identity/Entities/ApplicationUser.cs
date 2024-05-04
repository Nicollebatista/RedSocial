using Microsoft.AspNetCore.Identity;

namespace RedSocial.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }

    }
}
