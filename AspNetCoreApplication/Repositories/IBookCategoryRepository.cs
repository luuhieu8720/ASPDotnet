using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public interface IBookCategoryRepository
    {
        Task AddCategoryToBook(int bookId, int categoryId);
        Task DeleteBookCategory(int bookId, int categoryId);
        Task<List<Category>> GetCategoryByBookId(int bookId);
        Task DeleteCategory(int categoryId);
        Task<List<Book>> GetBookByCategoryId(int categoryId);
    }
}
