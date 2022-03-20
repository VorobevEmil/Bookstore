using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Data.Service
{
    public class BookModelService
    {
        private readonly AppDbContext _context;
        public BookModelService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<BookModel>> GetAll()
        {
            return await _context.Books
                .Include(t => t.Author)
                .Select(t => new BookModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Annotation = t.Annotation,
                    AboutProduct = t.AboutProduct,
                    NumberPages = t.NumberPages,
                    Price = t.Price,
                    YearPublication = t.YearPublication,
                    Author = new AuthorModel()
                    {
                        Id = t.Author.Id,
                        Title = t.Author.Title,
                        AboutAuthor = t.Author.AboutAuthor,
                    },
                    AuthorId = t.AuthorId,
                    Catalog = t.Catalog,
                    CatalogId = t.CatalogId,
                    PublishingHouse = t.PublishingHouse,
                    PublishingHouseId = t.PublishingHouseId,
                    FileData = t.FileData
                })
                .ToListAsync();
        }

        public async Task<BookModel> GetById(int id)
        {
            var result = await _context.Books
                .Include(t => t.Author)
                .Include(t => t.Catalog)
                .Include(t => t.PublishingHouse)
                .FirstAsync(t => t.Id == id);

            return new BookModel()
            {
                Id = result.Id,
                Title = result.Title,
                Annotation = result.Annotation,
                AboutProduct = result.AboutProduct,
                NumberPages = result.NumberPages,
                Price = result.Price,
                YearPublication = result.YearPublication,
                Author = new AuthorModel()
                {
                    Id = result.Author.Id,
                    Title = result.Author.Title,
                    AboutAuthor = result.Author.AboutAuthor,
                },
                AuthorId = result.AuthorId,
                Catalog = new CatalogModel()
                {
                    Id = result.Catalog.Id,
                    CatalogModelId = result.Catalog.CatalogModelId,
                    Catalogs = result.Catalog.Catalogs,
                    Title = result.Catalog.Title
                },
                CatalogId = result.CatalogId,
                PublishingHouse = new PublishingHouseModel()
                {
                    Id = result.PublishingHouse.Id,
                    Title = result.PublishingHouse.Title
                },
                PublishingHouseId = result.PublishingHouseId,
                FileData = result.FileData
            };
        }

        public async Task<List<BookModel>> GetAllByTitle(string title)
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
