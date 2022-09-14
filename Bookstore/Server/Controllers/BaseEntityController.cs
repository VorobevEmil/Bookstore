using Bookstore.Server.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseEntityController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        private readonly BaseService<TEntity> _service;

        public BaseEntityController(BaseService<TEntity> service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest("Произошла ошибка" + ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public virtual async Task<ActionResult<TEntity>> GetByIdAsync(int id)
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
        public async Task<IActionResult> SaveAsync(TEntity entity)
        {
            try
            {
                await _service.SaveAsync(entity);
                return Ok("Сущность сохранена");
            }
            catch (Exception ex)
            {
                return BadRequest("Не удалось сохранить сущность, сообщение об ошибке: " + ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync(TEntity entity)
        {
            try
            {
                await _service.Delete(entity);
                return Ok("Сущность удалена");
            }
            catch (Exception ex)
            {
                return BadRequest("Не удалось удалить сущность, сообщение об ошибке: " + ex.Message);
            }
        }
    }
}
