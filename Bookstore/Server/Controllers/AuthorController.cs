using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;

namespace Bookstore.Server.Controllers
{
    public class AuthorController : BaseEntityController<AuthorModel>
    {
        public AuthorController(BookstoreService<AuthorModel> service) : base(service)
        {
        }
    }
}
