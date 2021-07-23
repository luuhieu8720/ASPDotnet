using AspNetCoreApplication.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Extensions
{
    public static class StringExts
    {
        private const string passwordKey = "USpsD7LaKc27gmYG9TZCDUGb3MnAZatQJSUdLp9MkLNkq4MAj5qRYZ7zLFZa";

        public static string Encrypt(this string password)
        {
            password += passwordKey;
            var byteSourceText = Encoding.ASCII.GetBytes(password);
            var md5Hash = new MD5CryptoServiceProvider();
            var byteHash = md5Hash.ComputeHash(byteSourceText);

            return string.Concat(byteHash.Select(x => x.ToString("x2")));
        }
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
