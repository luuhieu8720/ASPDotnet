using AspNetCoreApplication.DTO.Category;
using AspNetCoreApplication.DTOcategory;
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

            return categories.Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetail>> Get(int id)
        {
            if (await dataContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync() is { } category)
            {
                return Ok(new CategoryDetail()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                });
            }
            return NotFound();
        }
        [HttpPost]
        public async Task Add([FromBody] CategoryForm categoryForm)
        {
            var category = new Category
            {
                Name = categoryForm.Name,
                Description = categoryForm.Description
            };
            dataContext.Categories.Add(category);
            await dataContext.SaveChangesAsync();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await dataContext.Categories.Where(category => category.Id == id).FirstOrDefaultAsync() is { } category)
            {
                dataContext.Categories.Remove(category);
                await dataContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryForm categoryCreate)
        {
            var category = await dataContext.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            category.Name = categoryCreate.Name;
            category.Description = categoryCreate.Description;
            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
