using AspNetCoreApplication.Config;
using AspNetCoreApplication.DTO.DTOUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Services
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate next;

        public TokenProviderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var authenticateInfo = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            var bearerTokenIdentity = authenticateInfo?.Principal;
            if (bearerTokenIdentity?.Identity is ClaimsIdentity identity)
            {
                context.User = new UserClaimsPrincipal(identity);
            }
            await next(context);
        }
    }
}
