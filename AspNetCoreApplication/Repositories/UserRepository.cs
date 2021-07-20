using AspNetCoreApplication.DTO.DTOUser;
using AspNetCoreApplication.Exceptions;
using AspNetCoreApplication.Extensions;
using AspNetCoreApplication.Mappings;
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
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task Create(UserForm userForm)
        {
            userForm.Password = userForm.Password.Encrypt();

            await dataContext.Users.AddAsync(userForm.ConvertTo<User>());

            await dataContext.SaveChangesAsync();
        }

        public async Task Update(int id, UserForm userForm)
        {
            var entry = await dataContext.Users.FindAsync(id) ??
                         throw new NotFoundException("User can't be found");

            userForm.Password = userForm.Password.Encrypt();
            userForm.CopyTo(entry);
            dataContext.Entry(entry).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }
    }
}
