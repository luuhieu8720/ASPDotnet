using AspNetCoreApplication.DTO.DTOuser;
using System;
using System.Collections.Generic;
using AspNetCoreApplication.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace AspNetCoreApplication.Services
{
    public class UserClaimsPrincipal : ClaimsPrincipal
    {
        public AuthenUser AuthenUser { get; private set; }
        public UserClaimsPrincipal(ClaimsIdentity claimsIdentity) : base(claimsIdentity)
        {
            AuthenUser = new AuthenUser()
            {
                Username = claimsIdentity.Claims.First(claim => claim.Type == ClaimTypes.Upn).Value,
                Name = claimsIdentity.Claims.First(claim => claim.Type == ClaimTypes.GivenName).Value,
                Id = claimsIdentity.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value,
                Role = claimsIdentity.Claims.First(claim => claim.Type == ClaimTypes.Role).Value
            };
        }
    }
}
