using IKIA.DAL.Models;
using IKIA.DAL.Presistence.Data;
using Microsoft.EntityFrameworkCore;

namespace IKIA.DAL.Presistence.Repositories._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbcontext _dbcontext;
        public GenericRepository(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return await _dbcontext.Set<T>().Where(x=>!x.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbcontext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbcontext.Set<T>().Where(x=>!x.IsDeleted);
        }

        public async Task<T?> GetAsync(int id)
        {
            var result = await _dbcontext.Set<T>().FindAsync(id);
            if (result is null || result.IsDeleted)
                return null;
            
            return result;
        }

        public void Add(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbcontext.Set<T>().Update(entity);
        }
    }
}
