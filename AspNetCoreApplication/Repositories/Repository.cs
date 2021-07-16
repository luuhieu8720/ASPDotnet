using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Mappings;
using AspNetCoreApplication.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly DataContext dataContext;
        public Repository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task Create(object source)
        {
            await dataContext.Set<T>().AddAsync(source.ConvertTo<T>());

            await dataContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entry = await GetByIdOrThrow(id);

            dataContext.Remove(entry);

            await dataContext.SaveChangesAsync();
        }

        public async Task<O> Get<O>(int id)
        {
            var entry = await GetByIdOrThrow(id);

            return entry.ConvertTo<O>();
        }

        public async Task<List<O>> Get<O>()
        {
            return await dataContext.Set<T>()
                         .Select(x => x.ConvertTo<O>())
                         .ToListAsync();
        }

        public async Task Update(int id, object source)
        {
            var entry = await GetByIdOrThrow(id);
            source.CopyTo(entry);
            dataContext.Entry(entry).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdOrThrow(int id)
        {
             return await dataContext.Set<T>().FindAsync(id) ??
                         throw new NotFoundException("Item can't be found");
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
            var entry = await GetByIdOrThrow(categoryId);

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
