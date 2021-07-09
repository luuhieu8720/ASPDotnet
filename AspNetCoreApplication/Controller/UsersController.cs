using AspNetCoreApplication.DTO.DTOuser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApplication.Mappings;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Models;

namespace AspNetCoreApplication.Controller
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext dataContext;
        public UsersController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet]
        public async Task<List<UserItem>> Get()
        {
            var categories = await dataContext.Users.ToListAsync();

            return categories.Select(x => x.MapTo<UserItem>()).ToList();
        }
        [HttpGet("{id}")]
        public async Task<UserDetail> Get(int id)
        {
            var author = await dataContext.Users.FindAsync(id) ??
                         throw new NotFoundException("User can't be found");

            return author.MapTo<UserDetail>();
        }
        [HttpPost]
        public async Task Add([FromBody] UserForm userForm)
        {
            var user = userForm.MapTo<User>();

            dataContext.Users.Add(user);

            await dataContext.SaveChangesAsync();
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var user = await dataContext.Users.FindAsync(id) ??
                         throw new NotFoundException("User can't be found");

            dataContext.Remove(user);
            await dataContext.SaveChangesAsync();
        }
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UserForm userForm)
        {
            var user = await dataContext.Users.FindAsync(id) ??
                             throw new NotFoundException("User can't be found");

            userForm.Copy(user);
            dataContext.Users.Attach(user);
            dataContext.Entry(user).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }
    }
}
