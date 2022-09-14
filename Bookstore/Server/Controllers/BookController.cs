using Bookstore.Server.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class BookController : BaseEntityController<BookModel>
    {
        private readonly BookService _service;
        public BookController(BookService service) : base(service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("GetBooksByCatalogId/{catalogId}")]
        public async Task<ActionResult<List<BookModel>>> GetBooksByCatalogIdAsync(int catalogId, int page, int sizepage)
        {
            try
            {
                return Ok(await _service.GetBooksByCatalogIdAsync(catalogId, page, sizepage));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAllByTitle/{title}")]
        public async Task<ActionResult<List<BookModel>>> GetAllByTitleAsync(string title)
        {
            try
            {
                return Ok(await _service.GetAllByTitleAsync(title));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetBooksOnOnePage")]
        public async Task<ActionResult<List<BookModel>>> GetBooksOnOnePageAsync(int page, int sizepage)
        {
            try
            {
                return Ok(await _service.GetBooksOnOnePageAsync(page, sizepage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
