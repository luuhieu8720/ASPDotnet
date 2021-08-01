using AspNetCoreApplication.Repositories;
using AspNetCoreApplication.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Configure
{
    public static class DIConfigure
    {
        public static void ConfigureDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICloudinaryService, CloudinaryCloudService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddHttpContextAccessor();
        }
    }
}
