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
    public class OrderController : ControllerBase
    {
        private readonly OrderCore _core;

        public OrderController(OrderCore core)
        {
            _core = core;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetAllAsync()
        {
            try
            {
                return Ok(await _core.GetAllAsync(User.Claims.First(t => t.Type == ClaimTypes.NameIdentifier).Value));
            }
            catch (Exception ex)
            {
                return BadRequest("Произошла ошибка" + ex);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<OrderModel>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _core.GetByIdAsync(id);
                if (result != null)
                    return Ok(result);

                return NotFound("Не удалось найти заказ");
            }
            catch (Exception ex)
            {
                return BadRequest("Произошла ошибка " + ex.Message);
            }
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync(OrderModel order)
        {
            try
            {
                await _core.SaveAsync(order);
                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync(OrderModel order)
        {
            try
            {
                await _core.DeleteAsync(order);
                return Ok("Сущность удалена");
            }
            catch (Exception ex)
            {
                return BadRequest("Не удалось удалить сущность, сообщение об ошибке: " + ex.Message);
            }
        }
    }
}
