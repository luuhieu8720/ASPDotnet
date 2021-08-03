using AspNetCoreApplication.Handlings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AspNetCoreApplication.Configure
{
    public static class FiltersConfigure
    {
        private static readonly List<Type> Filters = new()
        {
            typeof(HandleExceptionHandling),
            typeof(ValidateModelHandling),
        };

        public static void ConfigFilters(this MvcOptions options)
        {
            Filters.ForEach(f => options.Filters.Add(f));
        }
    }
}
