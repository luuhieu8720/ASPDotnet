using AspNetCoreApplication.DTO.DTObook;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Mappings;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Controller
{
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<List<BookItem>> Get() => await bookRepository.Get<BookItem>();

        [HttpGet("{id}")]
        public async Task<BookDetail> Get(int id) => await bookRepository.Get<BookDetail>(id);

        [HttpPost]
        public async Task Add([FromBody] BookForm bookForm) => await bookRepository.Create(bookForm);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await bookRepository.Delete(id);

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] BookForm bookForm) => await bookRepository.Update(id, bookForm);

    }
}
