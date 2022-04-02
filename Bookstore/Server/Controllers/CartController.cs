using Bookstore.Server.Core;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartCore _core;

        public CartController(CartCore core)
        {
            _core = core;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync(CartModel cart)
        {
            try
            {
                await _core.SaveAsync(cart);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
