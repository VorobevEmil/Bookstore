using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Service
{
    public class CatalogService : BaseService<CatalogModel>
    {
        public CatalogService(AppDbContext context) : base(context) { }

        public async Task<List<CatalogModel>> GetCatalogsParentAsync()
        {
            return await _context.Catalogs.Where(t => t.CatalogModelId == null).ToListAsync();
        }

        public async Task<List<CatalogModel>> GetCatalogsByParentAsync(int parentId)
        {
            return await _context.Catalogs.Where(t => t.CatalogModelId == parentId).ToListAsync();
        }
    }
}
