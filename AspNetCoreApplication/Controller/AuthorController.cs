using AspNetCoreApplication.DTO.DTOauthor;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Mappings;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
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
        private readonly IRepository<Author> author;
        public AuthorController(IRepository<Author> author)
        {
            this.author = author;
        }
        [HttpGet]
        public Task<List<AuthorItem>> Get()
        {
            return author.Get<AuthorItem>();
        }

        [HttpGet("{id}")]
        public Task<AuthorDetail> Get(int id)
        {
            return author.Get<AuthorDetail>(id);
        }
        [HttpPost]
        public Task Add([FromBody] AuthorForm authorForm)
        {
            return author.Add(authorForm.ConvertTo<Author>());
        }

        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return author.Delete(id);
        }
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] AuthorForm authorForm)
        {
            return author.Put(authorForm, id);
        }
    }
}
