using Bookstore.Shared.DbModels.DbInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    public class AuthorModel : BaseEntity, IFileManagement
    {
        [Required(ErrorMessage = "Полное имя автора обязательно для ввода")]
        public override string? Title { get; set; }
        public List<BookModel>? Books { get; set; }
        public string? AboutAuthor { get; set; }

        public string? FileData { get; set; }

    }
}
