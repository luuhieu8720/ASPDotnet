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
			ASCIIEncoding asciiEnc = new ASCIIEncoding();
			string hash = null;
			byte[] byteSourceText = asciiEnc.GetBytes(password);
			MD5CryptoServiceProvider md5Hash = new MD5CryptoServiceProvider();
			byte[] byteHash = md5Hash.ComputeHash(byteSourceText);

			foreach (byte b in byteHash)
			{
				hash += b.ToString("x2");
			}

			return hash;
		}
    }
}
