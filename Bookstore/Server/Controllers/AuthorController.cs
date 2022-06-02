using Bookstore.Server.Core;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class AuthorController : BaseEntityController<AuthorModel>
    {
        private readonly AuthorCore _core;
        public AuthorController(AuthorCore core) : base(core)
        {
            _core = core;
        }

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
    }
}
