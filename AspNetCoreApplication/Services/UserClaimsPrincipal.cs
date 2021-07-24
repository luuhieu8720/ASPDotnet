using AspNetCoreApplication.DTO.DTOuser;
using System;
using System.Collections.Generic;
using AspNetCoreApplication.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using AspNetCoreApplication.Extensions;
using AspNetCoreApplication.Services;

namespace AspNetCoreApplication.Services
{
    public class UserClaimsPrincipal : ClaimsPrincipal
    {
        public AuthenUser AuthenUser { get; private set; }
        public UserClaimsPrincipal(ClaimsIdentity claimsIdentity) : base(claimsIdentity)
        {
            AuthenUser = new AuthenUser(claimsIdentity);
        }
    }
}
