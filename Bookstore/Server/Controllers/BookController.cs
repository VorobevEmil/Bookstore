using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class BookController : BaseEntityController<BookModel>
    {
        public BookController(BookstoreService<BookModel> service) : base(service) { }
    }
}
