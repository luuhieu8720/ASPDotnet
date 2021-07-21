using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) :
            base(HttpStatusCode.Unauthorized, message)
        {

        }
    }
}
