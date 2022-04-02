using Bookstore.Server.Core;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class AuthorController : BaseEntityController<AuthorModel>
    {
        public AuthorController(AuthorCore core) :base(core) { }
    }
}
