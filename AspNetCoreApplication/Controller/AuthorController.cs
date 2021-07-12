using AspNetCoreApplication.DTO.DTOauthor;
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
            return authors.Select(x => x.ConvertTo<AuthorItem>()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<AuthorDetail> Get(int id)
        {
            var author = await dataContext.Authors.FindAsync(id) ??
                         throw new NotFoundException("Author can't be found");

            return author.ConvertTo<AuthorDetail>();
        }
        [HttpPost]
        public async Task Add([FromBody] AuthorForm authorForm)
        {
            var author = authorForm.ConvertTo<Author>();

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
                             throw new NotFoundException("User can't be found");

            authorForm.CopyTo(author);
            dataContext.Entry(author).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }
    }
}
