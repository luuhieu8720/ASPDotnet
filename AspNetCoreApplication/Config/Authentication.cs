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

namespace AspNetCoreApplication.Config
{
    public class Authentication : IAuthentication
    {
        private readonly TokenConfig tokenConfig;

        private readonly DataContext dataContext;

        public Authentication(TokenConfig tokenConfig, DataContext dataContext)
        {
            this.tokenConfig = tokenConfig;
            this.dataContext = dataContext;
        }

        public async Task<string> Post(string username, string password)
        {
            var checkUser = await dataContext.Users.Where(x => x.Username == username && x.Password == password)
                                                   .CountAsync();
            
            if (checkUser < 1) throw new NotFoundException("");

            var token = new JwtSecurityToken(tokenConfig.Issuer, tokenConfig.Audience);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
