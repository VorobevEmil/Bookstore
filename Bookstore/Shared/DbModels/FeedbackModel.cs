using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    public class FeedbackModel
    {
        [Key]
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Message { get; set; }
        public DateTime DateFeedback { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        public int BookOrderId { get; set; }
        [ForeignKey("BookOrderId")]
        public BookModelOrderModel? BookOrder { get; set; }
    }
}
