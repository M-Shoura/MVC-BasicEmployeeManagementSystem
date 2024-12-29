using IKIA.DAL.Common.Enums;
using IKIA.DAL.Models.Departments;

namespace IKIA.DAL.Models.Employees
{
    public class Employee : ModelBase
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string? Image { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
