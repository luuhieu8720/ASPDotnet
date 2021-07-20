using AspNetCoreApplication.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Extensions
{
    public static class StringExts
    {
        public static string Encrypt(this string password)
        {
            var tokenConfig = new TokenConfig();
            password += tokenConfig.Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
    }
}
