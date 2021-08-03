using AspNetCoreApplication.Config;
using AspNetCoreApplication.Configure;
using AspNetCoreApplication.Extensions;
using AspNetCoreApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.ConfigureDI();
            services.ConfigDatabase();
            services.AddControllers();
            services.AddHealthChecks();
            services.AddSwagger();
            services.AddMvc(FiltersConfigure.ConfigFilters).ConfigureJson();
            services.ConfigSecurity();
            services.MigrateDatabase();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.AddSwagger();
            app.UseHttpsRedirection();
            app.UseHealthChecks("/health");
            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMiddleware<TokenProviderMiddleware>();

            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
