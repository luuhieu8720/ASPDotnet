using AspNetCoreApplication.DTO.DTOCategory;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Handlings;
using AspNetCoreApplication.Services;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreApplication.Controller
{
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<Category> categoryRepository;

        private readonly ICategoryRepository categoryBookRepository;

        public CategoriesController(IRepository<Category> categoryRepository,
                                    ICategoryRepository categoryBookRepository)
        {
            this.categoryRepository = categoryRepository;
            this.categoryBookRepository = categoryBookRepository;
        }

        [HttpGet]
        public async Task<List<CategoryItem>> Get() => await categoryRepository.Get<CategoryItem>();

        [HttpGet("{id}")]
        public async Task<CategoryDetail> Get(int id) => await categoryRepository.Get<CategoryDetail>(id);

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task Add([FromBody] CategoryForm categoryForm) => await categoryRepository.Create(categoryForm);

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] CategoryForm categoryForm) => await categoryRepository.Update(id, categoryForm);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task DeleteCategory(int id) => await categoryBookRepository.Delete(id);
        
        [HttpGet("{id}/books")]
        public async Task<List<Book>> GetBooksByCategoryId(int id) => await categoryBookRepository.GetBooks(id);
    }
}
