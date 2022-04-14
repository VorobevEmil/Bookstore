using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Core
{
    public class BookOrderCore
    {
        private readonly AppDbContext _context;

        public BookOrderCore(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookModelOrderModel>> GetAllAsync(int orderId)
        {
            var result = await _context.BookModelOrderModel
                .Where(t => t.OrdersId == orderId)
                .Include(t => t.Books)
                .ToListAsync();

            result.ForEach(t => t.Books.Orders.ForEach(x => x.Orders = null));

            return result;
        }

        public async Task<BookModelOrderModel> GetByIdAsync(int id)
        {
            return await _context.BookModelOrderModel.FindAsync(id);
        }

        public async Task SaveAsync(BookModelOrderModel entity)
        {
            if (entity.Id == default)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BookModelOrderModel entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
