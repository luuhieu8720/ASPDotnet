using AspNetCoreApplication.Authentications;
using AspNetCoreApplication.Config;
using AspNetCoreApplication.Filter;
using AspNetCoreApplication.Handlings;
using AspNetCoreApplication.Models;
using AspNetCoreApplication.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer("name=ConnectionStrings:Connection")
            );
            services.AddControllers();
            services.AddMvc(ConfigMvc);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetCoreApplication", Version = "v1" });
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICloudinaryService, CloudinaryCloudService>();
            services.AddScoped<IAuthentication, Authentication>();
            services.AddScoped<IUserRepository, UserRepository>();
            ConfigType<ImageConfig>(services);
            ConfigType<TokenConfig>(services);
        }

        private T ConfigType<T>(IServiceCollection services)
        {
            var configObject = Activator.CreateInstance<T>();
            Configuration.Bind(typeof(T).Name, configObject);
            services.AddSingleton(typeof(T), configObject);
            return configObject;
        }

        private void ConfigMvc(MvcOptions options)
        {
            options.Filters.Add<HandleExceptionHandling>();
            options.Filters.Add(typeof(ValidateModel));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreApplication v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
