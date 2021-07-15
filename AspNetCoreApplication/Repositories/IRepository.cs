using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<List<O>> Get<O>();
        Task<O> Get<O>(int Id);
        Task Create(object source);
        Task Delete(int Id);
        Task Update(int id, object source);
        Task<T> GetByIdOrThrow(int id);
        Task AddCategoryToBook(int bookId, int categoryId);
        Task DeleteBookCategory(int bookId, int categoryId);
    }
}
