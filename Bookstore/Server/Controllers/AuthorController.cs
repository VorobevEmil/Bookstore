using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class AuthorController : BaseEntityController<AuthorModel>
    {
        private readonly AuthorModelService _authorService;

        public AuthorController(BookstoreService<AuthorModel> service, AuthorModelService authorService) : base(service)
        {
            _authorService = authorService;
        }

        [AllowAnonymous]
        [HttpGet("GetAuthorId/{id}")]
        public async Task<ActionResult<AuthorModel>> GetBookId(int id)
        {
            var result = await _authorService.GetAuthorById(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
