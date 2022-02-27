using Bookstore.Server.Data.Service;
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
        protected readonly BookstoreService<TEntity> _service;

        public BaseEntityController(BookstoreService<TEntity> service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("GetEntities")]
        public async Task<IEnumerable<TEntity>> GetEntities()
        {
            return await _service.GetListEntitiesAsync();
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TEntity>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost("Save")]
        public async Task Save(TEntity entity)
        {
            await _service.SaveAsync(entity);
        }

        [HttpPost("Delete")]
        public async Task Delete(TEntity entity)
        {
            await _service.Delete(entity);
        }
    }
}
