using AspNetCoreApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApplication.Configure
{
    public static class DatabaseConfigure
    {
        public static void ConfigDatabase(this IServiceCollection services)
        {
            services
                .AddDbContext<DataContext>(options => PostgresDatabaseConnection.ConfigPosgressDb(services, options));
        }

        public static void MigrateDatabase(this IServiceCollection services)
        {
            services.BuildServiceProvider()
                .GetService<DataContext>()
                .Database
                .Migrate();
        }
    }
}
