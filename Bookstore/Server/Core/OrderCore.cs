using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Core
{
    public class OrderCore
    {
        private readonly AppDbContext _context;

        public OrderCore(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderModel>> GetAllAsync(string? userId)
        {
            var orders = await _context.Orders
                .Where(t => t.UserId == userId)
                .Include(t => t.Books)
                .ThenInclude(t => t.Books)
                .OrderByDescending(t => t.OrderDate)
                .ToListAsync();

            orders.ForEach(t => t.Books.ForEach(x => x.Orders = null));
            orders.ForEach(t => t.Books.ForEach(x => x.Books.Orders = null));

            return orders;
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task SaveAsync(OrderModel entity)
        {
            if (entity.Id == default)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrderModel entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
