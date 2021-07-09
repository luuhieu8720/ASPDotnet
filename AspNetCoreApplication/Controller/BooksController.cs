using AspNetCoreApplication.DTO.DTObook;
using AspNetCoreApplication.DTO.DTOcategory;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Mappings;
using AspNetCoreApplication.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly DataContext dataContext;

        public BooksController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet]
        public async Task<List<BookItem>> Get()
        {
            var books = await dataContext.Books.ToListAsync();
            return books.Select(x => x.MapTo<BookItem>()).ToList();
        }
        [HttpGet("{id}")]
        public async Task<BookDetail> Get(int id)
        {
            var book = await dataContext.Books.FindAsync(id) ??
                           throw new NotFoundException("Book can't be found");

            return book.MapTo<BookDetail>();
        }
        [HttpPost]
        public async Task Add([FromBody] BookForm bookForm)
        {
            var book = bookForm.MapTo<Book>();

            dataContext.Books.Add(book);

            await dataContext.SaveChangesAsync();
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var book = await dataContext.Books.FindAsync(id) ??
                           throw new NotFoundException("Book can't be found");

            dataContext.Remove(book);

            await dataContext.SaveChangesAsync();
        }
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] BookForm bookForm)
        {
            var book = await dataContext.Books.FindAsync(id) ??
                             throw new NotFoundException("User can't be found");

            bookForm.Copy(book);
            dataContext.Books.Attach(book);
            dataContext.Entry(book).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }
    }
}
