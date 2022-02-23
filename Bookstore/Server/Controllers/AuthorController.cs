using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class AuthorController : BaseEntityController<AuthorModel>
    {
        public AuthorController(BookstoreService<AuthorModel> service) : base(service) { }
    }
}
