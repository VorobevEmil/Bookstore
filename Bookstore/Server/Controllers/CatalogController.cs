using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class CatalogController : BaseEntityController<CatalogModel>
    {
        public CatalogController(BookstoreService<CatalogModel> service) : base(service) { }
    }
}
