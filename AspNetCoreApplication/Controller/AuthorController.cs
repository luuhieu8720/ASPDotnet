using AspNetCoreApplication.DTO.DTOAuthor;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Handlings;
using AspNetCoreApplication.Services;
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
        private readonly IRepository<Author> authorRepository;
        public AuthorController(IRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<List<AuthorItem>> Get() => await authorRepository.Get<AuthorItem>();

        [HttpGet("{id}")]
        public async Task<AuthorDetail> Get(int id) => await authorRepository.Get<AuthorDetail>(id);

        [HttpPost]
        public async Task Add([FromBody] AuthorForm authorForm) => await authorRepository.Create(authorForm);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await authorRepository.Delete(id);

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] AuthorForm authorForm) => await authorRepository.Update(id, authorForm);
    }
}
