using Bookstore.Server.Data;
using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;
using Bookstore.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Controllers
{
    public class BookController : BaseEntityController<BookModel>
    {
        private readonly FileManagementService<BookModel> _fileManagementService;
        private readonly BookModelService _bookService;

        public BookController(BookstoreService<BookModel> service, FileManagementService<BookModel> fileManagementService, BookModelService bookService) : base(service)
        {
            _fileManagementService = fileManagementService;
            _bookService = bookService;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromBody] FileData fileData)
        {
            var errorMessage = await _fileManagementService.UploadFile(fileData, FilePath.BOOK);

            if (errorMessage != null)
                return Conflict(errorMessage);
            return Ok();
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
    }
}
