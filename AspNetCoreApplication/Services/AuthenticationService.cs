﻿using AspNetCoreApplication.DTO.DTOUser;
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

        public AuthenUser Get()
        {
            return new AuthenUser()
            {
                Id = httpContextAccessor.HttpContext.User.GetUserId(),
                Name = httpContextAccessor.HttpContext.User.GetName(),
                Username = httpContextAccessor.HttpContext.User.GetUsername(),
                Role = httpContextAccessor.HttpContext.User.GetRole()
            };
        }

        public async Task<TokenResponse> Login(string username, string password)
        {
            var user = await dataContext.Users
                            .FirstOrDefaultAsync(x => x.Username == username && x.Password == password.Encrypt())
                           ?? throw new BadRequestExceptions("Sai tên đăng nhập hoặc mật khẩu");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Upn, user.Username)
            };

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