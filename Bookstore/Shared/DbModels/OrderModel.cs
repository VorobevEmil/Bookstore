using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        public List<BookModelOrderModel>? Books { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
