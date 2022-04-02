using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Core
{
    public class BookCore : BaseCore<BookModel>
    {
        public BookCore(AppDbContext context) : base(context) { }
        public override async Task<BookModel> GetByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(t => t.Author)
                .Include(t => t.Catalog)
                .Include(t => t.PublishingHouse)
                .FirstAsync(t => t.Id == id);

            book.Author.Books = null;
            book.PublishingHouse.Books = null;
            book.Catalog.Books = null;
            return book;
        }
        public override async Task<List<BookModel>> GetAllAsync()
        {
            var books = await _context.Books
                .Include(t => t.Author)
                .ToListAsync();
            books.ForEach(t => t.Author.Books = null);
            return books;
        }

        public async Task<List<BookModel>> GetAllByTitleAsync(string title)
        {
            return await _context.Books
                .Where(t => t.Title.Contains(title))
                .Select(t => new BookModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                })
                .ToListAsync();
        }
    }
}
