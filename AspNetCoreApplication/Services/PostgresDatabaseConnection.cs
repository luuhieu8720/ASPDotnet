using AspNetCoreApplication.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Services
{
    public static class PostgresDatabaseConnection
    {
        private static PostgresConfig CreateHerokuConfig(IServiceCollection serviceCollection)
        {
            var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            if (string.IsNullOrWhiteSpace(connUrl))
            {
                return serviceCollection.BuildServiceProvider().GetService<PostgresConfig>(); ;
            }

            return PostgresConfig.Parse(connUrl);
        }

        public static void ConfigPosgressDb(IServiceCollection serviceCollection, DbContextOptionsBuilder options)
        {
            var config = CreateHerokuConfig(serviceCollection);

            options.UseNpgsql(config.BuildConnectionString());
        }
    }
}
