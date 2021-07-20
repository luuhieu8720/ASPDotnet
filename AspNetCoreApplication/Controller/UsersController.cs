﻿using AspNetCoreApplication.DTO.DTOUser;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Extensions;
using AspNetCoreApplication.Filter;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace AspNetCoreApplication.Controller
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> userRepository;

        private readonly IUserRepository repository;

        public UsersController(IRepository<User> userRepository, IUserRepository repository)
        {
            this.userRepository = userRepository;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<List<UserItem>> Get() => await userRepository.Get<UserItem>();

        [HttpGet("{id}")]
        public async Task<UserDetail> Get(int id) => await userRepository.Get<UserDetail>(id);

        [HttpPost]
        public async Task Add([FromBody] UserForm userForm) => await repository.Create(userForm);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await userRepository.Delete(id);

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] UserForm userForm) => await repository.Update(id, userForm);

    }
}
