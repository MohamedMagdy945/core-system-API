using UniversitySystem.Domain.Common;

namespace UniversitySystem.Domain.Entities.Business
{
    public class Department : BaseEntity<int>
    {
        public string Name = String.Empty;
        public string Description = String.Empty;
        public string Code = String.Empty;
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
