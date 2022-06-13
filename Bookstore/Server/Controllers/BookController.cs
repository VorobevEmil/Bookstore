using Bookstore.Server.Core;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class BookController : BaseEntityController<BookModel>
    {
        private readonly BookCore _core;
        public BookController(BookCore core) : base(core)
        {
            _core = core;
        }

        [AllowAnonymous]
        [HttpGet("GetBooksByCatalogId/{catalogId}")]
        public async Task<ActionResult<List<BookModel>>> GetBooksByCatalogIdAsync(int catalogId, int page, int sizepage)
        {
            try
            {
                return Ok(await _core.GetBooksByCatalogIdAsync(catalogId, page, sizepage));
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
                return Ok(await _core.GetAllByTitleAsync(title));
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
                return Ok(await _core.GetBooksOnOnePageAsync(page, sizepage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
