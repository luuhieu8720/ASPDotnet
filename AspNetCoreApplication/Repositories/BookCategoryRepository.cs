using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly DataContext dataContext;

        public BookCategoryRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task Add(int bookId, int categoryId)
        {
            var bookCategory = new BookCategory
            {
                BookId = bookId,
                CategoryId = categoryId,
            };

            await dataContext.BookCategories.AddAsync(bookCategory);

            await dataContext.SaveChangesAsync();

        }

        public async Task Delete(int bookId, int categoryId)
        {
            var entry = await dataContext.BookCategories.FirstOrDefaultAsync(x => x.BookId == bookId && x.CategoryId == categoryId);

            dataContext.Remove(entry);
            await dataContext.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategories(int bookId)
        {
            return await dataContext.BookCategories.Where(x => x.BookId == bookId)
                                                   .Select(x => x.Category)
                                                   .ToListAsync();
        }
    }
}
