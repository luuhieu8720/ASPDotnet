using AspNetCoreApplication.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Services
{
    public static class SecurityConfig
    {
        private static void SetJwtOption(JwtBearerOptions options, byte[] key)
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
            };
            options.SaveToken = true;
        }
        public static void ConfigSecurity(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes("USpsD7LaKc27gmYG9TZCDUGb3MnAZatQJSUdLp9MkLNkq4MAj5qRYZ7zLFZa");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => SetJwtOption(options, key));
        }
    }
}
