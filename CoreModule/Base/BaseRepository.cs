using CoreModule.DbContextConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Base
{
    public class BaseRepository<T> : BaseRepositoryInterface<T> where T : class
    {
        private readonly MyDbContext _context;
        public BaseRepository(MyDbContext context)
        {
            _context = context;
           
        }
        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity).ConfigureAwait(false);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
          return  await _context.Set<T>().FindAsync(id).ConfigureAwait(false);
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }

        public async Task  Remove(T entity)
        {
            await Task.Run(() => _context.Set<T>().Remove(entity)).ConfigureAwait(false); 
          
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            await Task.Run(()=>_context.Set<T>().RemoveRange(entities)).ConfigureAwait(false);
          
        }

        public async Task Update(T entity)
        {

            EntityEntry entry = _context.Entry<T>(entity);
            entry.State = EntityState.Modified;
        }
    }
}
