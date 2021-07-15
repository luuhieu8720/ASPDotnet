using AspNetCoreApplication.DTO.DTOUser;
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
        private readonly IRepository<User> userRepository;
        public UsersController(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet]
        public async Task<List<UserItem>> Get() => await userRepository.Get<UserItem>();

        [HttpGet("{id}")]
        public async Task<UserDetail> Get(int id) => await userRepository.Get<UserDetail>(id);

        [HttpPost]
        public async Task Add([FromBody] UserForm userForm) => await userRepository.Create(userForm);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await userRepository.Delete(id);

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] UserForm userForm) => await userRepository.Update(id, userForm);

    }
}
