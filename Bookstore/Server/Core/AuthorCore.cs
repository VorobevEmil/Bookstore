﻿using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Core
{
    public class AuthorCore : BaseCore<AuthorModel>
    {
        public AuthorCore(AppDbContext context) : base(context) { }

        public override async Task<AuthorModel> GetByIdAsync(int id)
        {
            var author = await _context.Authors
                .Include(t => t.Books)
                .FirstAsync(t => t.Id == id);

            return new AuthorModel()
            {
                Id = author.Id,
                AboutAuthor = author.AboutAuthor,
                FileData = author.FileData,
                Title = author.Title,
                Books = author.Books
                    .Select(t => new BookModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Price = t.Price,
                        FileData = t.FileData,
                    })
                    .ToList()
            };
        }
    }
}