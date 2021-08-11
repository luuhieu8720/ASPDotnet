using AspNetCoreApplication.Exceptions;
using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Handlings
{
    public class LoggerHandling : ExceptionFilterAttribute
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnException(ExceptionContext Context)
        {
            if (Context.Exception is HttpResponseException exception)
            {
                logger.Error(HttpStatusCode.InternalServerError);
                Context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.Status,
                };
                Context.ExceptionHandled = true;
            }
        }
    }
}
