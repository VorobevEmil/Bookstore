using System.ComponentModel.DataAnnotations;

namespace Bookstore.Shared.DbModels
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual string? Title { get; set; }
    }
}
