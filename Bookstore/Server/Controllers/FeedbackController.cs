using Bookstore.Server.Core;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookstore.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackCore _core;

        public FeedbackController(FeedbackCore core)
        {
            _core = core;
        }

        [AllowAnonymous]
        [HttpGet("GetFeedbacksByBookId/{bookId}")]
        public async Task<ActionResult<List<FeedbackModel>>> GetFeedbacksByBookId(int bookId)
        {
            try
            {
                return Ok(await _core.GetFeedbacksByBookId(bookId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync(FeedbackModel feedback)
        {
            try
            {
                await _core.SaveAsync(feedback);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
