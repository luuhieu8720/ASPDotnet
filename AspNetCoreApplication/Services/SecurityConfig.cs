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
        public static void ConfigSecurity(this IServiceCollection services, TokenConfig tokenConfig)
        {
            var key = Encoding.ASCII.GetBytes(tokenConfig.Key);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => SetJwtOption(options, key));
        }
    }
}
