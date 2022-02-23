using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    public class BookModel : BaseEntity
    {
        public string? AboutProduct { get; set; }
        public string? Annotation { get; set; }
        public int AuthorId { get; set; }
        public AuthorModel? Author { get; set; }
        public int PublishingHouseId { get; set; }
        public PublishingHouseModel? PublishingHouse { get; set; }
        public int CatalogId { get; set; }
        public CatalogModel? Catalog { get; set; }
        public int NumberPages { get; set; }
        public int YearPublication { get; set; }
        public int Price { get; set; }
    }
}
