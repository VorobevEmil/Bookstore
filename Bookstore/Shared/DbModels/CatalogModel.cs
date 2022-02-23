using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    public class CatalogModel : BaseEntity
    {
        [Required(ErrorMessage = "Название категории обязательно для заполнения")]
        public override string? Title { get; set; }
        public int? CatalogModelId { get; set; }
        public List<CatalogModel>? Catalogs { get; set; }
        public List<BookModel>? Books { get; set; }
    }
}
