using Bookstore.Server.Core;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class CatalogController : BaseEntityController<CatalogModel>
    {
        private readonly CatalogCore _core;
        public CatalogController(CatalogCore core) : base(core)
        {
            _core = core;
        }

        [AllowAnonymous]
        [HttpGet("GetCatalogsParent")]
        public async Task<ActionResult<List<CatalogModel>>> GetCatalogsParentAsync()
        {
            try
            {
                return Ok(await _core.GetCatalogsParentAsync());
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
                return Ok(await _core.GetCatalogsByParentAsync(parentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
