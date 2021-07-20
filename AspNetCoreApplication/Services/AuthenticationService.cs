using AspNetCoreApplication.DTO.DTOUser;
using AspNetCoreApplication.Exceptions;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using AspNetCoreApplication.Authentications;
using AspNetCoreApplication.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AspNetCoreApplication.Config;

namespace AspNetCoreApplication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly TokenConfig tokenConfig;

        private readonly DataContext dataContext;

        public AuthenticationService(TokenConfig tokenConfig, DataContext dataContext)
        {
            this.tokenConfig = tokenConfig;
            this.dataContext = dataContext;
        }

        public async Task<TokenResponse> Login(string username, string password)
        {
            var user = await dataContext.Users
                            .FirstOrDefaultAsync(x => x.Username == username && x.Password == password.Encrypt())
                           ?? throw new BadRequestExceptions("Sai tên đăng nhập hoặc mật khẩu");

            var claims = new[]
            {
                new Claim("Id",user.Id.ToString()),
                new Claim("Role",user.Role.ToString()),
                new Claim("Name",user.Name)
            };

            var tokenString = new JwtSecurityToken(tokenConfig.Issuer, tokenConfig.Audience, claims);

            return new TokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenString)
            };
        }
    }
}
