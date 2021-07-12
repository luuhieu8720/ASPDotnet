﻿using AspNetCoreApplication.DTO.DTOcategory;
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
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext dataContext;

        public CategoriesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<List<CategoryItem>> Get()
        {
            var categories = await dataContext.Categories.ToListAsync();

            return categories.Select(x => x.ConvertTo<CategoryItem>()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<CategoryDetail> Get(int id)
        {
            var category = await dataContext.Categories.FindAsync(id) ??
                           throw new NotFoundException("Category can't be found");

            return category.ConvertTo<CategoryDetail>();
        }
        [HttpPost]
        public async Task Add([FromBody] CategoryForm categoryForm)
        {
            var category = categoryForm.ConvertTo<Category>();

            dataContext.Categories.Add(category);

            await dataContext.SaveChangesAsync();
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var category = await dataContext.Categories.FindAsync(id) ??
                           throw new NotFoundException("Category can't be found");

            dataContext.Remove(category);

            await dataContext.SaveChangesAsync();
        }
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CategoryForm categoryForm)
        {
            var category = await dataContext.Categories.FindAsync(id) ??
                             throw new NotFoundException("Category can't be found");

            categoryForm.CopyTo(category);
            dataContext.Entry(category).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }
    }
}
