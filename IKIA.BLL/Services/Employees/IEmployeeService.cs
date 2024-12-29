using IKIA.BLL.DTOs.Employees;

namespace IKIA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync(string search);
        Task<EmployeeDetailsDTO?> GetEmployeeByIdAsync(int employeeId);
        Task<int> CreateEmployeeAsync(CreateEmployeeDTO employee);
        Task<int> UpdateEmployeeAsync(UpdateEmployeeDTO employee);
        Task<bool> DeleteEmployeeAsync(int employeeId);
    }
}
