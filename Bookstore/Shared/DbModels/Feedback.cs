using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    // TODO доделать модель отзывов
    public class Feedback
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Message { get; set; }
        public DateTime DateFeedback { get; set; }
    }
}
