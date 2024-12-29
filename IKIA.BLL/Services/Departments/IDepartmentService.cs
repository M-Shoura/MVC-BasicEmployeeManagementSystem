using IKIA.BLL.DTOs.Departments;

namespace IKIA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsToReturnDTO?> GetDepartmentByIdAsync(int departmentId);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDTO departmentDTO);
        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDTO departmentDTO);
        Task<bool> DeleteDepartmentAsync(int departmentId);
    }
}
