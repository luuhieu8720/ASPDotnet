using AspNetCoreApplication.DTO.Author;
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
            return authors.Select(x => new AuthorItem()
            {
                Name = x.Name,
                Birthday = x.Birthday,
                Cover = x.Cover,
                Website = x.Website
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetail>> Get(int id)
        {
            if(await dataContext.Authors.Where(author => author.Id == id).FirstOrDefaultAsync() is { } author)
            {
                return Ok(new AuthorDetail() { 
                    Name = author.Name,
                    Cover = author.Cover,
                    Birthday = author.Birthday,
                    Website = author.Website
                });
            }
            return NotFound();
        }
        [HttpPost]
        public async Task Add([FromBody] AuthorCreate authorCreate)
        {
            var author = new Author
            {
                Name = authorCreate.Name,
                Website = authorCreate.Website,
                Cover = authorCreate.Cover,
                Birthday = authorCreate.Birthday
            };

            dataContext.Authors.Add(author);

            await dataContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await dataContext.Authors.Where(author => author.Id == id).FirstOrDefaultAsync() is { } author)
            {
                dataContext.Authors.Remove(author);
                await dataContext.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AuthorCreate authorCreate)
        {
            var author = await dataContext.Authors.FindAsync(id);

            if (author == null)
                return NotFound();

            author.Name = authorCreate.Name;
            author.Cover = authorCreate.Cover;
            author.Birthday = authorCreate.Birthday;
            author.Website = authorCreate.Website;
            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
