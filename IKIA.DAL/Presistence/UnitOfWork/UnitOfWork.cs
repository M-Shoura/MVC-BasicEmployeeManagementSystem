using IKIA.DAL.Presistence.Data;
using IKIA.DAL.Presistence.Repositories.Departments;
using IKIA.DAL.Presistence.Repositories.Employees;

namespace IKIA.DAL.Presistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbcontext _dbcontext;

        public IEmployeeRepository EmployeeRepository { get { return new EmployeeRepository(_dbcontext); } }
        public IDepartmentRepository DepartmentRepository { get { return new DepartmentRepository(_dbcontext); } }

        public UnitOfWork(ApplicationDbcontext dbcontext)
        {    
            _dbcontext = dbcontext;
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }
        public  async ValueTask DisposeAsync()
        {
           await _dbcontext.DisposeAsync();
        }
    }
}
