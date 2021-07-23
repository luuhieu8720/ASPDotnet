using AspNetCoreApplication.DTO.DTOUser;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Extensions;
using AspNetCoreApplication.Services;
using AspNetCoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public async Task Create(UserForm userForm)
        {
            userForm.Password = userForm.Password.Encrypt();

            await base.Create(userForm);
        }

        public async Task Update(int id, UserForm userForm)
        {
            userForm.Password = userForm.Password.Encrypt();
            await base.Update(id, userForm);
        }
    }
}
