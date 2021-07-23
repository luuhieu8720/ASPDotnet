using AspNetCoreApplication.Extensions;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOuser
{
    public class AuthenUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public Role Role { get; set; }

        public AuthenUser(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Username = user.Username;
            Role = user.Role;
        }

        public AuthenUser(ClaimsIdentity claimsIdentity)
        {
            Id = int.Parse(claimsIdentity.GetClaimValue(ClaimTypes.NameIdentifier));
            Name = claimsIdentity.GetClaimValue(ClaimTypes.GivenName);
            Username = claimsIdentity.GetClaimValue(ClaimTypes.Upn);
            Role = claimsIdentity.GetClaimValue(ClaimTypes.Role).ToEnum<Role>();
        }

        public Claim[] GetClaims()
        {
            return httpContextAccessor.HttpContext.User.Claims.ToArray();
        }
    }
}
