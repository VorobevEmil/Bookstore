using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    public class Question
    {
        public int Id { get; set; }
        public Question? QuestionParent { get; set; }
    }
}
