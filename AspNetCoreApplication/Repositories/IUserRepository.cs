using AspNetCoreApplication.DTO.DTOUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Repositories
{
    public interface IUserRepository
    {
        Task Create(UserForm userForm);
        Task Update(int id, UserForm userForm);
    }
}
