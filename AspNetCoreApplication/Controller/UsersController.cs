using AspNetCoreApplication.DTO.DTOUser;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Extensions;
using AspNetCoreApplication.Handlings;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository repository)
        {
            this.userRepository = repository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<List<UserItem>> Get() => await userRepository.Get<UserItem>();

        [Authorize]
        [HttpGet("{id}")]
        public async Task<UserDetail> Get(int id) => await userRepository.Get(id);

        [HttpPost]
        public async Task Add([FromBody] UserForm userForm) => await userRepository.Create(userForm);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task Delete(int id) => await userRepository.Delete(id);

        [Authorize]
        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] UserForm userForm) => await userRepository.Update(id, userForm);

    }
}
