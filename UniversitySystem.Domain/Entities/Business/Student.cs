using UniversitySystem.Domain.Common;

namespace UniversitySystem.Domain.Entities.Business
{
    public class Student : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string PersonalEmail { get; set; } = string.Empty;
        public string? UniversityEmail { get; set; } = null;
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public double GPA { get; set; }
        public int Level { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
