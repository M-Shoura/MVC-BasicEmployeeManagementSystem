using System.ComponentModel.DataAnnotations;

namespace IKIA.BLL.DTOs.Employees
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string Gender { get; set; } = null!;
        public string EmployeeType { get; set; } = null!;
        public string? Department { get; set; } = null!;
        public string? Image { get; set; }
    }
}
