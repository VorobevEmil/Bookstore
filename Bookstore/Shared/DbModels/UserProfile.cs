using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    public class UserProfile
    {
        public string IdUser { get; set; } = null!;
        public int Age { get; set; }
        public string Sex { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string? ThirdName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegDate { get; set; }
        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
    }
}
