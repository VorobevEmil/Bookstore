using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Shared.DbModels
{
    public class BookModelOrderModel
    {
        [Key]
        public int Id { get; set; }
        public int BooksId { get; set; }
        [ForeignKey("BooksId")]
        public BookModel? Books { get; set; }
        public int OrdersId { get; set; }
        [ForeignKey("OrdersId")]
        public OrderModel? Orders { get; set; }

        public List<FeedbackModel>? Feedbacks { get; set; }
    }
}
