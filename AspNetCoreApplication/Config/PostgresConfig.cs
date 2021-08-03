using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Config
{
    public class PostgresConfig
    {
        public string Host { get; set; }

        public string Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string DatabaseName { get; set; }

        public bool Secure { get; set; } = false;

        public bool TrustConnection { get; set; } = false;

        public string BuildConnectionString()
        {
            var connectionString = $"Server={Host};Port={Port};User Id={Username};Password={Password};Database={DatabaseName}";

            if (Secure)
            {
                connectionString += ";sslmode=Require";
            }

            if (TrustConnection)
            {
                connectionString += ";Trust Server Certificate=true";
            }

            return connectionString;
        }

        public static PostgresConfig Parse(string url)
        {
            var connUrl = url.Replace("postgres://", string.Empty);
            var pgUserPass = connUrl.Split("@")[0];
            var pgHostPortDb = connUrl.Split("@")[1];
            var pgHostPort = pgHostPortDb.Split("/")[0];

            return new PostgresConfig()
            {
                Host = pgHostPort.Split(":")[0],
                Port = pgHostPort.Split(":")[1],
                Username = pgUserPass.Split(":")[0],
                Password = pgUserPass.Split(":")[1],

                DatabaseName = pgHostPortDb.Split("/")[1],
                Secure = true,
                TrustConnection = true
            };
        }
    }
}
