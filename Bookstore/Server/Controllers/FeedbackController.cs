using Bookstore.Server.Service;
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
        private readonly FeedbackService _service;

        public FeedbackController(FeedbackService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("GetFeedbacksByBookId/{bookId}")]
        public async Task<ActionResult<List<FeedbackModel>>> GetFeedbacksByBookId(int bookId)
        {
            try
            {
                return Ok(await _service.GetFeedbacksByBookId(bookId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetFeedbacksByUserId")]
        public async Task<ActionResult<List<FeedbackModel>>> GetFeedbacksByUserId()
        {
            try
            {
                return Ok(await _service.GetFeedbacksByUserId(User.Claims.First(t => t.Type == ClaimTypes.NameIdentifier).Value));
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
                await _service.SaveAsync(feedback);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
