using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Data.Service
{
    public class BookstoreService<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;

        public BookstoreService(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<List<TEntity>> GetListEntitiesAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int? id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public virtual async Task SaveAsync(TEntity entity)
        {
            if (entity.Id == default)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
