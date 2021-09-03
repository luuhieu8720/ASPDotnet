using AspNetCoreApplication.Handlings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Configure
{
    public static class JsonConfigure
    {
        public static void ConfigureJson(this IMvcBuilder builder)
        {
            builder.AddNewtonsoftJson(ConfigJson);
        }

        private static void ConfigJson(MvcNewtonsoftJsonOptions jsonOptions)
        {
            jsonOptions.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
            jsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
