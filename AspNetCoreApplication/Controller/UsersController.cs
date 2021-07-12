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
using AspNetCoreApplication.Repositories;

namespace AspNetCoreApplication.Controller
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> user;
        public UsersController(IRepository<User> user)
        {
            this.user = user;
        }
        [HttpGet]
        public Task<List<UserItem>> Get()
        {
            return user.Get<UserItem>();
        }
        [HttpGet("{id}")]
        public Task<UserDetail> Get(int id)
        {
            return user.Get<UserDetail>(id);
        }
        [HttpPost]
        public Task Add([FromBody] UserForm userForm)
        {
            return user.Add(userForm.ConvertTo<User>());
        }
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return user.Delete(id);
        }
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] UserForm userForm)
        {
            return user.Put(userForm, id);
        }
    }
}
