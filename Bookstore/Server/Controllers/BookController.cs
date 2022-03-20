using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class BookController : BaseEntityController<BookModel>
    {
        private readonly BookModelService _bookService;

        public BookController(BookstoreService<BookModel> service, BookModelService bookService) : base(service)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet("GetBooks")]
        public async Task<List<BookModel>> GetBooks()
        {
            return await _bookService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("GetBookId/{id}")]
        public async Task<ActionResult<BookModel>> GetBookId(int id)
        {
            var result = await _bookService.GetById(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("GetBooksByTitle/{title}")]
        public async Task<List<BookModel>> GetBooksByTitle(string title)
        {
            return await _bookService.GetAllByTitle(title);
        }
    }
}
