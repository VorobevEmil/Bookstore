using Microsoft.AspNetCore.Identity;

namespace Bookstore.Shared.DbModels
{
    public class ApplicationUser : IdentityUser
    {
        public List<CartModel>? Carts { get; set; }
    }
}
