using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Exceptions
{
    public class InternalServerException : BaseException
    {
        public InternalServerException(string message) :
            base(HttpStatusCode.InternalServerError, message)
        {

        }
    }
}
