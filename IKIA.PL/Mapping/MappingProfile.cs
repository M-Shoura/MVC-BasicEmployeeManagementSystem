using AutoMapper;
using IKIA.BLL.DTOs.Departments;
using IKIA.PL.ViewModels.Departments;

namespace IKIA.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Department

            CreateMap<DepartmentViewModel,CreatedDepartmentDTO>();
            CreateMap<DepartmentViewModel,UpdatedDepartmentDTO>();
            CreateMap<DepartmentDetailsToReturnDTO,DepartmentViewModel>();

            #endregion

            #region Employee


            #endregion
        }
    }
}
