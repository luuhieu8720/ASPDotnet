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
        public async Task AddCategoryToBook(int bookId, int categoryId)
        {
            var category = await dataContext.Categories.FindAsync(categoryId);

            var book = await dataContext.Books.FindAsync(bookId);

            var bookCategory = new BookCategory
            {
                BookId = bookId,
                Book = book,
                CategoryId = categoryId,
                Category = category
            };

            await dataContext.BookCategories.AddAsync(bookCategory);

            await dataContext.SaveChangesAsync();

        }

        public async Task DeleteBookCategory(int bookId, int categoryId)
        {
            var entry = await dataContext.BookCategories.Where(x => x.BookId == bookId && x.CategoryId == categoryId)
                                                        .Select(x => x)
                                                        .FirstOrDefaultAsync();

            dataContext.Remove(entry);
            await dataContext.SaveChangesAsync();
        }
        public async Task<List<Category>> GetCategoryByBookId(int bookId)
        {
            return await dataContext.BookCategories.Where(x => x.BookId == bookId)
                                                   .Select(x => x.Category)
                                                   .ToListAsync();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var entry = await dataContext.Categories.FindAsync(categoryId) ??
                         throw new NotFoundException("Item can't be found");

            var categoryOfBook = await dataContext.BookCategories.Where(x => x.CategoryId == categoryId)
                                                                 .Select(x => x).ToListAsync();
            dataContext.BookCategories.RemoveRange(categoryOfBook);
            dataContext.Remove(entry);

            await dataContext.SaveChangesAsync();
        }
        public async Task<List<Book>> GetBookByCategoryId(int categoryId)
        {
            return await dataContext.BookCategories.Where(x => x.CategoryId == categoryId)
                                                   .Select(x => x.Book)
                                                   .ToListAsync();
        }
    }
}
