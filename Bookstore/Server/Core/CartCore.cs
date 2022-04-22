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

        public async Task<List<CartModel>> GetAllAsync(string? userId)
        {
            var cart = await _context.Carts
                .Where(t => t.UserId == userId)
                .Include(t => t.Book)
                .ThenInclude(t => t.Author)
                .Include(t => t.Book)
                .ThenInclude(t => t.PublishingHouse)
                .ToListAsync();

            cart.ForEach(t => t.Book.Carts = null);
            cart.ForEach(t => t.Book.Author.Books = null);
            cart.ForEach(t => t.Book.PublishingHouse.Books = null);

            return cart;
        }

        public async Task<CartModel> GetByIdAsync(int id)
        {
            return await _context.Carts.FindAsync(id);
        }

        public async Task SaveAsync(CartModel entity)
        {
            if (entity.Id == default)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CartModel entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
