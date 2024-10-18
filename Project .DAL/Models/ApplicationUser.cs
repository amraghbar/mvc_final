using Microsoft.AspNetCore.Identity;

namespace Project_.DAL.Models
{
	public class ApplicationUser : IdentityUser
    {
        public string? City { get; set; }
        public string? Gender { get; set; }
    }
}
