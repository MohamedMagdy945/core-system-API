using Microsoft.AspNetCore.Identity;

namespace AppCoreSystem.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string? FullName { get; set; }
        public string? NationalId { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
