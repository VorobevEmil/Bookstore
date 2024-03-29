﻿using Bookstore.Shared.DbModels.DbInterfaces;
using System.ComponentModel.DataAnnotations;

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
