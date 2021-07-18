using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public interface IBookCategoryRepository
    {
        Task Add(int bookId, int categoryId);

        Task Delete(int bookId, int categoryId);

    }
}
