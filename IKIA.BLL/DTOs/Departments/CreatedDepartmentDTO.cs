using System.ComponentModel.DataAnnotations;

namespace IKIA.BLL.DTOs.Departments
{
    public class CreatedDepartmentDTO
    {
        [Required(ErrorMessage = "Code is Required Ya Hamada !!")]
        public string Name { get; set; } = null!;
        
        public string Code { get; set; } = null!;
        public string? Description { get; set; }


        [Display(Name = "Date of Creationnnnnnn")]
        public DateOnly CreationDate { get; set; }
    }
}
