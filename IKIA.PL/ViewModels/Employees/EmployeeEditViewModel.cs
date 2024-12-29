namespace IKIA.PL.ViewModels.Employees
{
    public class EmployeeEditViewModel
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public string Gender { get; set; }
        public string EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
    }
}
