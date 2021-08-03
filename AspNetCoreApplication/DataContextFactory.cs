using AspNetCoreApplication.Config;
using AspNetCoreApplication.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var postgressConfig = configuration.CreateConfig<PostgresConfig>();

            var sqlConnection = configuration.GetValue<string>("ConnectionStrings:Connection");
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseNpgsql(postgressConfig.BuildConnectionString());
            return new DataContext(optionsBuilder.Options);
        }
    }
}
