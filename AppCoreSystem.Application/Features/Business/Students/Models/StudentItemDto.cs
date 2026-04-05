namespace AppCoreSystem.Application.Features.Business.Students.Models
{
    public class StudentItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PersonalEmail { get; set; } = string.Empty;
        public int Level { get; set; }
        public int DepartmentId { get; set; }
    }
}
