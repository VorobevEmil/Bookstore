using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;

namespace Bookstore.Server.Core
{
    public class CatalogCore : BaseCore<CatalogModel>
    {
        public CatalogCore(AppDbContext context) : base(context)
        {
        }
    }
}
