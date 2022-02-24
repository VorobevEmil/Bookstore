﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Shared.DbModels
{
    public class BookModel : BaseEntity
    {
        [Required(ErrorMessage = "Название книги обязательно для ввода")]
        public override string? Title {get; set;}
        [Required(ErrorMessage = "Напишите описание книги")]
        public string? AboutProduct { get; set; }
        public string? Annotation { get; set; }
        [Required(ErrorMessage = "Выберите автора")]
        public int? AuthorId { get; set; }
        public AuthorModel? Author { get; set; }
        [Required(ErrorMessage = "Выберите издание")]
        public int? PublishingHouseId { get; set; }
        public PublishingHouseModel? PublishingHouse { get; set; }
        [Required(ErrorMessage = "Выберите категорию")]
        public int? CatalogId { get; set; }
        public CatalogModel? Catalog { get; set; }
        [Required(ErrorMessage = "Введите количество страниц")]
        public int? NumberPages { get; set; }
        [Required(ErrorMessage = "Введите год издания")]
        public int? YearPublication { get; set; }
        [Required(ErrorMessage = "Введите цену")]
        public int? Price { get; set; }

        public byte[]? ImageData { get; set; }
    }
}
