using IKIA.DAL.Presistence.Repositories.Departments;
using IKIA.DAL.Presistence.Repositories.Employees;

namespace IKIA.DAL.Presistence.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository EmployeeRepository { get;}
        public IDepartmentRepository DepartmentRepository { get;}
        Task<int> CompleteAsync();
    }
}
