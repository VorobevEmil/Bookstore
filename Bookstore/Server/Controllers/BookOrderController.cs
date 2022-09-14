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
    public class BookOrderController : ControllerBase
    {
        private readonly BookOrderCore _service;

        public BookOrderController(BookOrderCore service)
        {
            _service = service;
        }

        [HttpGet("GetBooksOrdersForWhichUserHasNotLeftReview")]
        public async Task<ActionResult<List<BookModelOrderModel>>> GetBooksOrdersForWhichUserHasNotLeftReview()
        {
            try
            {
                return Ok(await _service.GetBooksOrdersForWhichUserHasNotLeftReview(User.Claims.First(t => t.Type == ClaimTypes.NameIdentifier).Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAll/{orderId}")]
        public async Task<ActionResult<IEnumerable<BookModelOrderModel>>> GetAllAsync(int orderId)
        {
            try
            {
                return Ok(await _service.GetAllAsync(orderId));
            }
            catch (Exception ex)
            {
                return BadRequest("Произошла ошибка" + ex);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BookModelOrderModel>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result != null)
                    return Ok(result);

                return NotFound("Не удалось найти сущность");
            }
            catch (Exception ex)
            {
                return BadRequest("Произошла ошибка " + ex.Message);
            }
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync(BookModelOrderModel order)
        {
            try
            {
                await _service.SaveAsync(order);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync(BookModelOrderModel order)
        {
            try
            {
                await _service.DeleteAsync(order);
                return Ok("Сущность удалена");
            }
            catch (Exception ex)
            {
                return BadRequest("Не удалось удалить сущность, сообщение об ошибке: " + ex.Message);
            }
        }
    }
}
