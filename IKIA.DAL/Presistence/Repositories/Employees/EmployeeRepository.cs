using IKIA.DAL.Models.Employees;
using IKIA.DAL.Presistence.Data;
using IKIA.DAL.Presistence.Repositories._Generic;

namespace IKIA.DAL.Presistence.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbcontext dbcontext) : base(dbcontext)
        {

        }
    }
}
