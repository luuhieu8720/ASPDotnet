using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetClaimValue(this ClaimsPrincipal user, string claimType)
        {
            return user.FindFirst(claimType)?.Value;
        }
    }
}
