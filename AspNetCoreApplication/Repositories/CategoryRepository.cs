using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task Delete(int categoryId)
        {
            var entry = await dataContext.Categories.FindAsync(categoryId) ??
                         throw new NotFoundException("Category can't be found");

            dataContext.Remove(entry);

            await dataContext.SaveChangesAsync();
        }

        public async Task<List<Book>> GetBooks(int categoryId)
        {
            return await dataContext.Categories.Where(x => x.Id == categoryId)
                .SelectMany(x => x.Books.Select(y => y.Book))
                .ToListAsync();
        }
    }
}
