using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace AspNetCoreApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var portConfig = Environment.GetEnvironmentVariable("PORT");

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureKestrel(serverOptions =>
                    {
                        if (!string.IsNullOrEmpty(portConfig))
                        {
                            serverOptions.Listen(IPAddress.Any, Convert.ToInt32(portConfig));
                        }
                    })
                    .UseStartup<Startup>();
                });
        }
    }
}
