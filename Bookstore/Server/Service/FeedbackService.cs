using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Service
{
    public class FeedbackService
    {
        private readonly AppDbContext _context;

        public FeedbackService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FeedbackModel>> GetFeedbacksByBookId(int bookId)
        {
            var result = await _context.Feedbacks
                .Include(t => t.User)
                .Where(t => t.BookOrder.BooksId == bookId)
                .OrderByDescending(t => t.DateFeedback)
                .ToListAsync();

            result.ForEach(t => t.User.Feedbacks = null);
            return result;
        }

        public async Task<List<FeedbackModel>> GetFeedbacksByUserId(string userId)
        {
            var result = await _context.Feedbacks
                .Include(t => t.User)
                .Include(t => t.BookOrder)
                .ThenInclude(t => t.Books)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.DateFeedback)
                .ToListAsync();

            result.ForEach(t => t.BookOrder.Feedbacks = null);
            result.ForEach(t => t.BookOrder.Books.Orders = null);
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
