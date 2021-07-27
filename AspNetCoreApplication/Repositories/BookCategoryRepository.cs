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
    public class BookCategoryRepository : Repository<Book>, IBookCategoryRepository
    {
        private readonly DataContext dataContext;

        private readonly IAuthenticationService authenticationService;

        public BookCategoryRepository(DataContext dataContext, IAuthenticationService authenticationService) : base(dataContext)
        {
            this.dataContext = dataContext;
            this.authenticationService = authenticationService;
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
            await CheckRole(bookId);
            var entry = await dataContext.BookCategories.FirstOrDefaultAsync(x => x.BookId == bookId && x.CategoryId == categoryId) ??
                            throw new NotFoundException("BookCategory can't be found");

            dataContext.Remove(entry);

            await dataContext.SaveChangesAsync();
        }

        public async Task CheckRole(int id)
        {
            var currentUser = authenticationService.CurrentUser;
            var bookDetail = await base.GetByIdOrThrow(id);

            var listRole = new Role[2] { Role.Admin, Role.Manager };
            if (!listRole.Contains(currentUser.Role)
                && bookDetail.AuthorId != currentUser.Id)
            {
                throw new UnauthorizedException("Không có quyền truy cập");
            }
        }
    }
}
