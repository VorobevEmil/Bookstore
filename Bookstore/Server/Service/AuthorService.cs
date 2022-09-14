using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Service
{
    public class AuthorService : BaseService<AuthorModel>
    {
        public AuthorService(AppDbContext context) : base(context) { }

        public override async Task<AuthorModel> GetByIdAsync(int id)
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

        public async Task<List<AuthorModel>> GetAllByTitleAsync(string title)
        {
            return await _context.Authors
                .Where(t => t.Title.Contains(title))
                .Select(t => new AuthorModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                })
                .ToListAsync();
        }
    }
}
