using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;
using Bookstore.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class BookController : BaseEntityController<BookModel>
    {
        private readonly FileManagementService<BookModel> _fileManagementService;

        public BookController(BookstoreService<BookModel> service, FileManagementService<BookModel> fileManagementService) : base(service)
        {
            _fileManagementService = fileManagementService;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromBody] FileData fileData)
        {
            var errorMessage = await _fileManagementService.UploadFile(fileData, FilePath.BOOK);

            if (errorMessage != null)
                return Conflict(errorMessage);
            return Ok();
        }
    }
}
