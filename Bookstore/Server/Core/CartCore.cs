using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Core
{
    public class CartCore
    {
        private readonly AppDbContext _context;

        public CartCore(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<List<CartModel>> GetListEntitiesAsync()
        {
            return await _context.Carts.ToListAsync();
        }

        public virtual async Task<CartModel> GetByIdAsync(int id)
        {
            return await _context.Carts.FindAsync(id);
        }

        public virtual async Task SaveAsync(CartModel entity)
        {
            if (entity.Id == default)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(CartModel entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
