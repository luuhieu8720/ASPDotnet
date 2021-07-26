using AspNetCoreApplication.DTO.DTOBook;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Handlings;
using AspNetCoreApplication.Services;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IBookCategoryRepository bookCategoryRepository;

        public BooksController(IBookRepository bookRepository, IBookCategoryRepository bookCategoryRepository)
        {
            this.bookRepository = bookRepository;
            this.bookCategoryRepository = bookCategoryRepository;
        }

        [HttpGet]
        public async Task<List<BookItem>> Get() => await bookRepository.Get<BookItem>();

        [HttpGet("{id}")]
        public async Task<BookDetail> Get(int id) => await bookRepository.Get<BookDetail>(id);

        [Authorize]
        [HttpPost]
        public async Task Add([FromBody] BookForm bookForm) => await bookRepository.Create(bookForm);

        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id) => await bookRepository.Delete(id);

        [Authorize]
        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] BookForm bookForm) => await bookRepository.Update(id, bookForm);
        
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/categories/{categoryId}")]
        public async Task AddCategory(int id, int categoryId)
            => await bookCategoryRepository.Add(id, categoryId);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/categories/{categoryId}")]
        public async Task Delete(int id, int categoryId) => await bookCategoryRepository.Delete(id, categoryId);
        
        [HttpGet("{id}/categories")]
        public async Task<List<Category>> GetCategories(int id)
            => await bookRepository.GetCategories(id);
    }
}
