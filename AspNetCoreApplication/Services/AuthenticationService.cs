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
using AspNetCoreApplication.DTO.DTOuser;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreApplication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly TokenConfig tokenConfig;

        private readonly DataContext dataContext;

        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticationService(TokenConfig tokenConfig, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            this.tokenConfig = tokenConfig;
            this.dataContext = dataContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public UserClaimsPrincipal GetCurrentUser()
        {
            return (UserClaimsPrincipal)httpContextAccessor.HttpContext.User;
        }

        public async Task<TokenResponse> Login(string username, string password)
        {
            var user = await dataContext.Users
                            .FirstOrDefaultAsync(x => x.Username == username && x.Password == password.Encrypt())
                           ?? throw new BadRequestExceptions("Sai tên đăng nhập hoặc mật khẩu");

            var claims = new AuthenUser(user).GetClaims();

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig.Key));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var tokenString = new JwtSecurityToken(tokenConfig.Issuer, tokenConfig.Audience, claims: claims, signingCredentials: signingCredentials);

            return new TokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenString)
            };
        }
    }
}
