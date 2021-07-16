using AspNetCoreApplication.DTO.DTOCategory;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Mappings;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Controller
{
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<Category> categoryRepository;

        private readonly IBookCategoryRepository bookCategoryRepository;

        private readonly ICategoryRepository categoryBookRepository;

        public CategoriesController(IRepository<Category> categoryRepository,
                                    IBookCategoryRepository bookCategoryRepository, 
                                    ICategoryRepository categoryBookRepository)
        {
            this.categoryRepository = categoryRepository;
            this.bookCategoryRepository = bookCategoryRepository;
            this.categoryBookRepository = categoryBookRepository;
        }

        [HttpGet]
        public async Task<List<CategoryItem>> Get() => await categoryRepository.Get<CategoryItem>();

        [HttpGet("{id}")]
        public async Task<CategoryDetail> Get(int id) => await categoryRepository.Get<CategoryDetail>(id);

        [HttpPost]
        public async Task Add([FromBody] CategoryForm categoryForm) => await categoryRepository.Create(categoryForm);

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] CategoryForm categoryForm) => await categoryRepository.Update(id, categoryForm);
        
        [HttpDelete("{categoryId}")]
        public async Task DeleteCategory(int id) => await categoryBookRepository.Delete(id);
        
        [HttpGet("{categoryId}/books")]
        public async Task<List<Book>> GetBooksByCategoryId(int categoryId) => await categoryBookRepository.GetBooks(categoryId);
    }
}
