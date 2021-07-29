using AspNetCoreApplication.Config;
using AspNetCoreApplication.Extensions;
using AspNetCoreApplication.Handlings;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using AspNetCoreApplication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigType<ImageConfig>(Configuration);
            services.ConfigType<PostgresConfig>(Configuration);
            services.ConfigType<TokenConfig>(Configuration);

            services
                .AddDbContext<DataContext>(options => PostgresDatabaseConnection.ConfigPosgressDb(services, options));

            services.AddControllers();
            services.AddHealthChecks();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStoreAPI", Version = "v1" });
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICloudinaryService, CloudinaryCloudService>();
            services.AddScoped<Services.IAuthenticationService, Services.AuthenticationService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddHttpContextAccessor();

            services.AddMvc(ConfigMvc);
            services.ConfigSecurity();

#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            services.BuildServiceProvider()
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
                .GetService<DataContext>()
                .Database
                .Migrate();
        }

        

        private void ConfigMvc(MvcOptions options)
        {
            options.Filters.Add<HandleExceptionHandling>();
            options.Filters.Add<ValidateModelHandling>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore"));

            app.UseHttpsRedirection();
            app.UseHealthChecks("/health");
            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMiddleware<TokenProviderMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
