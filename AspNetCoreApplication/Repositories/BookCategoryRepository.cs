using AspNetCoreApplication.DTO.DTOuser;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly DataContext dataContext;

        private readonly IBookRepository bookRepository;

        public BookCategoryRepository(DataContext dataContext, 
            IBookRepository bookRepository)
        {
            this.dataContext = dataContext;
            this.bookRepository = bookRepository;
        }

        public async Task Add(int bookId, int categoryId)
        {
            await bookRepository.CheckRole(bookId);
            var bookCategory = new BookCategory
            {
                BookId = bookId,
                CategoryId = categoryId,
            };
            try
            {
                await dataContext.BookCategories.AddAsync(bookCategory);

                await dataContext.SaveChangesAsync();
            }
            catch
            {
                throw new BadRequestException("Bad request");
            }

        }

        public async Task Delete(int bookId, int categoryId)
        {
            await bookRepository.CheckRole(bookId);
            var entry = await dataContext.BookCategories.FirstOrDefaultAsync(x => x.BookId == bookId && x.CategoryId == categoryId) ??
                            throw new NotFoundException("BookCategory can't be found");

            dataContext.Remove(entry);

            await dataContext.SaveChangesAsync();
        }
    }
}
