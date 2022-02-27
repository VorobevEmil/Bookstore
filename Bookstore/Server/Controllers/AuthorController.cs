using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;
using Bookstore.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class AuthorController : BaseEntityController<AuthorModel>
    {
        private readonly FileManagementService<AuthorModel> _fileManagementService;

        public AuthorController(BookstoreService<AuthorModel> service, FileManagementService<AuthorModel> fileManagementService) : base(service)
        {
            _fileManagementService = fileManagementService;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromBody] FileData fileData)
        {
            var errorMessage = await _fileManagementService.UploadFile(fileData, FilePath.AUTHOR);

            if (errorMessage != null)
                return Conflict(errorMessage);
            return Ok();
        }
    }
}
