using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Exceptions
{
    public class BaseException: Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public BaseException(HttpStatusCode statusCode, string message):
            base(message)
        {
            StatusCode = statusCode;
        }
    }
}
