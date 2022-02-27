using System.ComponentModel.DataAnnotations;

namespace Bookstore.Shared.DbModels
{
    public class PublishingHouseModel : BaseEntity
    {
        [Required(ErrorMessage = "Название издательства обязательно для ввода")]
        public override string? Title {get; set;}
        public List<BookModel>? Books { get; set; }
    }
}
