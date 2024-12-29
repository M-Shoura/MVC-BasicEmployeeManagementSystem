using IKIA.DAL.Models;

namespace IKIA.DAL.Presistence.Repositories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true);
        IQueryable<T> GetAllAsIQueryable();
        Task<T?> GetAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T id);
    }
}
