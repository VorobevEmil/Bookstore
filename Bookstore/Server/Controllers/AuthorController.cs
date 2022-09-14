using Bookstore.Server.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class AuthorController : BaseEntityController<AuthorModel>
    {
        private readonly AuthorService _service;
        public AuthorController(AuthorService service) : base(service)
        {
            _service = service;
        }

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
    }
}
