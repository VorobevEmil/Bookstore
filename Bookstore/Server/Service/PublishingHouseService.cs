using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Service
{
    public class PublishingHouseService : BaseService<PublishingHouseModel>
    {
        public PublishingHouseService(AppDbContext context) : base(context) { }

        public override async Task<PublishingHouseModel> GetByIdAsync(int id)
        {
            var result = await _context.PublishingHouses
                .Include(t => t.Books)
                .ThenInclude(t => t.Author)
                .FirstAsync(t => t.Id == id);

            result.Books.ForEach(t => t.PublishingHouse = null);
            result.Books.ForEach(t => t.Author.Books = null);


            return result;
        }
    }
}
