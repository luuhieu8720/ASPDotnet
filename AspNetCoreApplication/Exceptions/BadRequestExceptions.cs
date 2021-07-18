using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Exceptions
{
    public class BadRequestExceptions : BaseException
    {
        public BadRequestExceptions(string message) :
            base(HttpStatusCode.BadRequest, message)
        {

        }
    }
}
