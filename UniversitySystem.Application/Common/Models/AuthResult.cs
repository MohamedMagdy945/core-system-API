namespace UniversitySystem.Application.Common.Models
{
    public class AuthResult
    {
        public string? UserId { get; set; }
        public List<string> Errors { get; set; } = new();
        public bool IsSuccess => Errors.Count == 0;
    }
}
