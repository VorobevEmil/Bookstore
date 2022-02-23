using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    public class PublishingHouseModel : BaseEntity
    {
        [Required(ErrorMessage = "Название издательства обязательно для ввода")]
        public override string? Title {get; set;}
        public List<BookModel>? Books { get; set; }
    }
}
