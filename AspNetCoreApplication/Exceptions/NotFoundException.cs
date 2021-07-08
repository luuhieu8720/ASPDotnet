using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Exceptions
{
    public class NotFoundException: BaseException
    {
        public NotFoundException(string message):
            base(HttpStatusCode.NotFound, message)
        {

        }
    }
}
