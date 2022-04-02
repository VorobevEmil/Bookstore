using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Shared.DbModels
{
    public class CartModel
    {
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        public BookModel? Book { get; set; }
        [Required]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}
