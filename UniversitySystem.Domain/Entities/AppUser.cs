using Microsoft.AspNetCore.Identity;

namespace UniversitySystem.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? FullName { get; set; }
        public string? NationalId { get; set; }
    }
}
