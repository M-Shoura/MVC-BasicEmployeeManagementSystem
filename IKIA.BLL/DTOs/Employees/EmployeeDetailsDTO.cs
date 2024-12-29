using IKIA.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKIA.BLL.DTOs.Employees
{
    public class EmployeeDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        
        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        
        public string? Email { get; set; }
        

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        
        public Gender Gender { get; set; } 
        public EmployeeType EmployeeType { get; set; } 
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int? DepartmentId { get; set; }
        public string? Department { get; set; } = null!;
        public string? Image { get; set; }

    }
}
