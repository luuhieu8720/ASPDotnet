using AspNetCoreApplication.DTO.Author;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Mappings;
using AspNetCoreApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Controller
{
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly DataContext dataContext;

        public AuthorController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<List<AuthorItem>> Get()
        {
            var authors = await dataContext.Authors.ToListAsync();
            return authors.Select(x => x.MapTo<AuthorItem>()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<AuthorDetail> Get(int id)
        {
            var author = await dataContext.Authors.FindAsync(id) ??
                         throw new NotFoundException("Author can't be found");

            return author.MapTo<AuthorDetail>();
        }
        [HttpPost]
        public async Task Add([FromBody] AuthorForm authorForm)
        {
            var author = authorForm.MapTo<Author>();

            dataContext.Authors.Add(author);

            await dataContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var author = await dataContext.Authors.FindAsync(id) ??
                         throw new NotFoundException("Author can't be found");

            dataContext.Remove(author);
            await dataContext.SaveChangesAsync();

        }
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] AuthorForm authorForm)
        {
            var author = await dataContext.Authors.FindAsync(id) ??
                         throw new NotFoundException("Author can't be found");
            
            author = authorForm.MapTo<Author>();
            dataContext.Entry(author).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }
    }
}
