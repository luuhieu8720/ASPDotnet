using AspNetCoreApplication.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreApplication.Filter
{
    public class ValidateModel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(modelState => modelState.Errors).ToList();
                
                var errorMessage = errors.Select(x => x.ErrorMessage).FirstOrDefault();

                throw new BadRequestExceptions(errorMessage);
            }
        }
    }
}
