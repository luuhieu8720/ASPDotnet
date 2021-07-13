using AspNetCoreApplication.DTO.DTOcategory;
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

        public CategoriesController(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<List<CategoryItem>> Get() => await categoryRepository.Get<CategoryItem>();

        [HttpGet("{id}")]
        public async Task<CategoryDetail> Get(int id) => await categoryRepository.Get<CategoryDetail>(id);

        [HttpPost]
        public async Task Add([FromBody] CategoryForm categoryForm) => await categoryRepository.Create(categoryForm);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await categoryRepository.Delete(id);

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] CategoryForm categoryForm) => await categoryRepository.Update(id, categoryForm);

    }
}
