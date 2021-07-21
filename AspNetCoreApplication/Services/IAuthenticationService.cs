using AspNetCoreApplication.Authentications;
using AspNetCoreApplication.DTO.DTOuser;
using AspNetCoreApplication.DTO.DTOUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> Login(string username, string password);
        CurrentUser GetUserFromToken(string token);
        Task SetCurrentUser(string username, string password);
        ClaimsPrincipal GetCurrentUser();
    }
}
