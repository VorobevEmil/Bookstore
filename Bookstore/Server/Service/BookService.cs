using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Service
{
    public class BookService : BaseService<BookModel>
    {
        public BookService(AppDbContext context) : base(context) { }
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
        public async Task<List<BookModel>> GetBooksOnOnePageAsync(int page, int sizepage)
        {
            var books = await _context.Books
                .Include(t => t.Author)
                .Skip(page * sizepage)
                .Take(sizepage)
                .ToListAsync();
            books.ForEach(t => t.Author.Books = null);
            return books;
        }

        public async Task<List<BookModel>> GetBooksByCatalogIdAsync(int catalogId, int page, int sizepage)
        {
            var catalog = await _context.Catalogs.Include(t => t.Catalogs).FirstAsync(t => t.Id == catalogId);

            var books = await GetBooksByCatalog(new List<CatalogModel>() { catalog });
            books.ForEach(t => t.Author.Books = null);
            books.ForEach(t => t.Catalog.Books = null);

            return books.Skip(page * sizepage).Take(sizepage).ToList();

        }

        private async Task<List<BookModel>> GetBooksByCatalog(List<CatalogModel> catalogs)
        {
            List<BookModel> books = new List<BookModel>();

            foreach (var catalog in catalogs)
            {
                if (catalog.Catalogs == null)
                {
                    catalog.Catalogs = await _context.Catalogs.Where(t => t.CatalogModelId == catalog.Id).ToListAsync();
                }
                if (catalog.Catalogs.Count != default)
                {
                    books.AddRange(await GetBooksByCatalog(catalog.Catalogs));
                }
                else
                {
                    books.AddRange(await _context.Books.Include(t => t.Author).Where(t => t.CatalogId == catalog.Id).ToListAsync());
                }
            }

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
