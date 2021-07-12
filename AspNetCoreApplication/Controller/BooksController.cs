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
        private readonly IRepository<Book> book;
        public BooksController(IRepository<Book> book)
        {
            this.book = book;
        }
        private readonly DataContext dataContext;

        [HttpGet]
        public Task<List<BookItem>> Get()
        {
            return book.Get<BookItem>();
        }
        [HttpGet("{id}")]
        public Task<BookDetail> Get(int id)
        {
            return book.Get<BookDetail>(id);
        }
        [HttpPost]
        public Task Add([FromBody] BookForm bookForm)
        {
            return book.Add(bookForm.ConvertTo<Book>());
        }
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return book.Delete(id);
        }
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] BookForm bookForm)
        {
            return book.Put(bookForm, id);
        }
    }
}
