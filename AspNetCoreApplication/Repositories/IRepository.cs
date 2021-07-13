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
        Task Add(object source);
        Task Delete(int Id);
        Task Put(object source, int id);
    }
}
