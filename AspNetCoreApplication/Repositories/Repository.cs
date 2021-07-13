using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly DataContext dataContext;

        public Repository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task Create(object source)
        {
            await dataContext.Set<T>().AddAsync(source.ConvertTo<T>());

            await dataContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entry = GetByIdOrThrow(id);

            dataContext.Remove(entry);

            await dataContext.SaveChangesAsync();
        }

        public async Task<O> Get<O>(int id)
        {
            var entry = await GetByIdOrThrow(id);

            return entry.ConvertTo<O>();
        }

        public async Task<List<O>> Get<O>()
        {
            return await dataContext.Set<T>()
                         .Select(x => x.ConvertTo<O>())
                         .ToListAsync();
        }

        public async Task Update(int id, object source)
        {
            var entry = GetByIdOrThrow(id);

            source.CopyTo(entry);
            dataContext.Entry(entry).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdOrThrow(int id)
        {
             return await dataContext.Set<T>().FindAsync(id) ??
                         throw new NotFoundException("Item can't be found");
        }
    }
}
