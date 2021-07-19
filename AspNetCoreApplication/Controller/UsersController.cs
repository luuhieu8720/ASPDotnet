using AspNetCoreApplication.DTO.DTOUser;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Filter;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [ValidateModel]
        public async Task Add([FromBody] UserForm userForm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(modelState => modelState.Errors).ToList();
                var errorMessage = errors.Select(x => x.ErrorMessage).ToList().Aggregate("", (current, next) => current + ", " + next);

                throw new BadRequestExceptions(errorMessage);
            }

            await userRepository.Create(userForm);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await userRepository.Delete(id);

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task Update(int id, [FromBody] UserForm userForm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(modelState => modelState.Errors).ToList();
                var errorMessage = errors.Select(x => x.ErrorMessage).ToList().Aggregate("", (current, next) => current + ", " + next);

                throw new BadRequestExceptions(errorMessage);
            }

            await userRepository.Update(id, userForm);
        }

    }
}
