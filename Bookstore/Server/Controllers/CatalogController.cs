using Bookstore.Server.Core;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class CatalogController : BaseEntityController<CatalogModel>
    {
        public CatalogController(CatalogCore core) :base(core) { }
    }
}
