using Bookstore.Server.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class CatalogController : BaseEntityController<CatalogModel>
    {
        private readonly CatalogService _service;
        public CatalogController(CatalogService service) : base(service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("GetCatalogsParent")]
        public async Task<ActionResult<List<CatalogModel>>> GetCatalogsParentAsync()
        {
            try
            {
                return Ok(await _service.GetCatalogsParentAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetCatalogsByParent/{parentId}")]
        public async Task<ActionResult<List<CatalogModel>>> GetCatalogsByParentAsync(int parentId)
        {
            try
            {
                return Ok(await _service.GetCatalogsByParentAsync(parentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
