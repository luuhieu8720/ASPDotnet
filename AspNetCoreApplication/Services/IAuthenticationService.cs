using AspNetCoreApplication.DTO.DTOAuths;
using AspNetCoreApplication.DTO.DTOuser;
using AspNetCoreApplication.DTO.DTOUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> Login(string username, string password);

        AuthenUser CurrentUser { get; }

        int CurrentUserId { get; }
    }
}
