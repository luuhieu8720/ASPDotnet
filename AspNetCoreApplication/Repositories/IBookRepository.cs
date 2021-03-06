using AspNetCoreApplication.DTO.DTOBook;
using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task Create(BookForm source);

        Task Update(int id, BookForm source);

        Task<List<Category>> GetCategories(int bookId);

        Task CheckRole(int id);

        Task<List<BookItem>> Get();

        Task <BookDetail> Get(int id);
    }
}
