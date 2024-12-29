using IKIA.DAL.Models.Departments;
using System.ComponentModel.DataAnnotations;

namespace IKIA.BLL.DTOs.Departments
{
    public class DepartmentToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;


        [Display(Name = "Date of Creation")] 
        public DateOnly CreationDate { get; set; }


        public static explicit operator DepartmentToReturnDTO(Department department)
        {
            return new DepartmentToReturnDTO()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate,
            };
        }
    }
}
