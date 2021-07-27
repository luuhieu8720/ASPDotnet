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

        private readonly IAuthenticationService authenticationService;

        private readonly IBookRepository bookRepository;

        public BookCategoryRepository(DataContext dataContext, 
            IAuthenticationService authenticationService,
            IBookRepository bookRepository)
        {
            this.dataContext = dataContext;
            this.authenticationService = authenticationService;
            this.bookRepository = bookRepository;
        }

        public async Task Add(int bookId, int categoryId)
        {
            await CheckRole(bookId);
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

        public async Task CheckRole(int bookId)
        {
            var currentUser = authenticationService.CurrentUser;
            var bookDetail = await bookRepository.GetByIdOrThrow(bookId);

            if (currentUser.Role == Role.Admin) return;
            if (currentUser.Role == Role.Manager) return;

            if (bookDetail.AuthorId != currentUser.Id)
            {
                throw new UnauthorizedException("Bạn không có quyền hạn thay đổi sách này.");
            }
        }
    }
}
