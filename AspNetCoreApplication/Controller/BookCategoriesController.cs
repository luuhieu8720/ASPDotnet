using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Controller
{
    [Route("api/books/")]
    [ApiController]
    public class BookCategoriesController : ControllerBase
    {
        private readonly IRepository<Book> bookRepository;
        public BookCategoriesController(IRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        [HttpPost("{bookId}/categories/{categoryId}")]
        public async Task Create(int bookId, int categoryId) 
        => await bookRepository.AddCategoryToBook(bookId, categoryId);

        [HttpDelete("{bookId}/categories/{categoryId}")]
        public async Task Delete(int bookId, int categoryId) => await bookRepository.DeleteBookCategory(bookId, categoryId);

    }
}
