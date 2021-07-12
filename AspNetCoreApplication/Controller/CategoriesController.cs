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
        private readonly IRepository<Category> category;

        public CategoriesController(IRepository<Category> category)
        {
            this.category = category;
        }

        [HttpGet]
        public Task<List<CategoryItem>> Get()
        {
            return category.Get<CategoryItem>();
        }

        [HttpGet("{id}")]
        public Task<CategoryDetail> Get(int id)
        {
            return category.Get<CategoryDetail>(id);
        }
        [HttpPost]
        public Task Add([FromBody] CategoryForm categoryForm)
        {
            return category.Add(categoryForm.ConvertTo<Category>());
        }
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return category.Delete(id);
        }
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] CategoryForm categoryForm)
        {
            return category.Put(categoryForm, id);
        }
    }
}
