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

        public BookCategoryRepository(DataContext dataContext, IAuthenticationService authenticationService)
        {
            this.dataContext = dataContext;
            this.authenticationService = authenticationService;
        }

        public async Task Add(int bookId, int categoryId)
        {
            CheckRoleAdmin();
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
            CheckRoleAdmin();
            var entry = await dataContext.BookCategories.FirstOrDefaultAsync(x => x.BookId == bookId && x.CategoryId == categoryId) ??
                            throw new NotFoundException("BookCategory can't be found");

            dataContext.Remove(entry);

            await dataContext.SaveChangesAsync();
        }

        public void CheckRoleAdmin()
        {
            var currentUser = authenticationService.CurrentUser;
            if (currentUser.Role != Role.Admin)
            {
                throw new UnauthorizedException("Không có quyền truy cập");
            }
        }
    }
}
