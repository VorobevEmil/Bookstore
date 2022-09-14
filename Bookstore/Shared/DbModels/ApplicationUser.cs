using Microsoft.AspNetCore.Identity;

namespace Bookstore.Shared.DbModels
{
    public class ApplicationUser : IdentityUser
    {
        public List<CartModel>? Carts { get; set; }
        public List<OrderModel>? Orders { get; set; }
        public List<FeedbackModel>? Feedbacks { get; set; }

        public UserProfile Profile { get; set; } = null!;
    }
}
