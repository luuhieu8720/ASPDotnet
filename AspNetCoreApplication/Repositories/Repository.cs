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

        public async Task Add(object source)
        {
            await dataContext.Set<T>().AddAsync(source.ConvertTo<T>());

            await dataContext.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entry = await dataContext.Set<T>().FindAsync(Id) ??
                           throw new NotFoundException("Item can't be found");

            dataContext.Remove(entry);

            await dataContext.SaveChangesAsync();
        }

        public async Task<O> Get<O>(int Id)
        {
            var entry = await dataContext.Set<T>().FindAsync(Id) ??
                              throw new NotFoundException("Item can't be found");

            return entry.ConvertTo<O>();
        }

        public async Task<List<O>> Get<O>()
        {
            return await dataContext.Set<T>()
                         .Select(x => x.ConvertTo<O>())
                         .ToListAsync();
        }

        public async Task Put(object source, int id)
        {
            var entry = await dataContext.Set<T>().FindAsync(id) ??
                               throw new NotFoundException("Item can't be found");

            source.CopyTo(entry);
            entry.Id = id;
            dataContext.Entry(entry).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }
    }
}
