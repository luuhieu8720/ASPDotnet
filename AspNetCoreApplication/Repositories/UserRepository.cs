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
using Microsoft.AspNetCore.Http;
using AspNetCoreApplication.DTO.DTOuser;

namespace AspNetCoreApplication.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly IAuthenticationService authenticationService;

        public UserRepository(DataContext dataContext, 
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationService authenticationService) : base(dataContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.authenticationService = authenticationService;
        }

        public async Task Create(UserForm userForm)
        {
            userForm.Password = userForm.Password.Encrypt();

            await base.Create(userForm);
        }

        public async Task Update(int id, UserForm userForm)
        {
            var currentUserId = authenticationService.CurrentUserId;
            if (id != currentUserId) throw new UnauthorizedException("Không có quyền truy cập");
            userForm.Password = userForm.Password.Encrypt();
            await base.Update(id, userForm);
        }
    }
}
