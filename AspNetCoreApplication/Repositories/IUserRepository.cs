using AspNetCoreApplication.DTO.DTOUser;
using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task Create(UserForm userForm);
        Task Update(int id, UserForm userForm);
    }
}
