using AspNetCoreApplication.DTO.DTObook;
using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        string CheckForUploading(string urlOrBase64);
        Task Create(BookForm source);
        Task Update(int id, BookForm source);
    }
}
