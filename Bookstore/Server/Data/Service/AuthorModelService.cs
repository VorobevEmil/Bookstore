using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Data.Service
{
    public class AuthorModelService
    {
        private AppDbContext _context;

        public AuthorModelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorModel> GetAuthorById(int id)
        {
            var author = await _context.Authors
                .Include(t => t.Books)
                .FirstAsync(t => t.Id == id);

            return new AuthorModel()
            {
                Id = author.Id,
                AboutAuthor = author.AboutAuthor,
                FileData = author.FileData,
                Title = author.Title,
                Books = author.Books
                    .Select(t => new BookModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Price = t.Price,
                        FileData = t.FileData,
                    })
                    .ToList()
            };
        }
    }
}
