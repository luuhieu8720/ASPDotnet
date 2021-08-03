using AspNetCoreApplication.Exceptions;
using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Services
{
    public class LogFilter : ExceptionFilterAttribute
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnException(ExceptionContext Context)
        {
            logger.Error(HttpStatusCode.InternalServerError);
            throw new InternalServerException("Internal server exception");
        }
    }
}
