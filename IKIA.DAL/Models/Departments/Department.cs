using IKIA.DAL.Models.Employees;

namespace IKIA.DAL.Models.Departments
{
    public class Department : ModelBase
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
