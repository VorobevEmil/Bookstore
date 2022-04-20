using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Core
{
    public class FeedbackCore
    {
        private readonly AppDbContext _context;

        public FeedbackCore(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FeedbackModel>> GetFeedbacksByBookId(int bookId)
        {
            var result = await _context.Feedbacks
                .Include(t => t.User)
                .Where(t => t.BookOrder.BooksId == bookId)
                .ToListAsync();

            result.ForEach(t => t.User.Feedbacks = null);
            return result;
        }

        public async Task SaveAsync(FeedbackModel entity)
        {
            if (entity.Id == default)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
