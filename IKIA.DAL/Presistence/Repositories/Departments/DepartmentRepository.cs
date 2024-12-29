using IKIA.DAL.Models.Departments;
using IKIA.DAL.Presistence.Data;
using IKIA.DAL.Presistence.Repositories._Generic;

namespace IKIA.DAL.Presistence.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbcontext dbcontext) : base(dbcontext)
        {

        }
    }
}
