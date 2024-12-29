using IKIA.BLL.DTOs.Departments;
using IKIA.DAL.Models.Departments;
using IKIA.DAL.Presistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKIA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsIQueryable().Select(department => new DepartmentToReturnDTO()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToListAsync();

            return departments;
        }

        public async Task<DepartmentDetailsToReturnDTO?> GetDepartmentByIdAsync(int departmentId)
        {
            var dept = await _unitOfWork.DepartmentRepository.GetAsync(departmentId);
            if (dept is not null)
            {
                return new DepartmentDetailsToReturnDTO()
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Code = dept.Code,
                    CreatedBy = dept.CreatedBy,
                    CreatedOn = dept.CreatedOn,
                    CreationDate = dept.CreationDate,
                    Description = dept.Description,
                    LastModifiedBy = dept.LastModifiedBy,
                    LastModifiedOn = dept.LastModifiedOn
                };
            }
            return null;
        }

        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDTO departmentDTO)
        {
            var department = new Department()
            {
                Name = departmentDTO.Name,
                Code = departmentDTO.Code,
                CreationDate = departmentDTO.CreationDate,
                Description = departmentDTO.Description,

                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,   
                IsDeleted = false
            };

            _unitOfWork.DepartmentRepository.Add(department);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDTO departmentDTO)
        {
            var department = new Department()
            {
                Id = departmentDTO.Id,
                Name = departmentDTO.Name,
                Code = departmentDTO.Code,
                Description = departmentDTO.Description,
                CreationDate = departmentDTO.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            _unitOfWork.DepartmentRepository.Update(department);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int departmentId)
        {
            var UOF =  _unitOfWork.DepartmentRepository;
            var department = await UOF.GetAsync(departmentId);
            if (department is { })
                UOF.Delete(department);

            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
