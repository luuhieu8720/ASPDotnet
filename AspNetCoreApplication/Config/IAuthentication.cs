using AspNetCoreApplication.DTO.DTOUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Config
{
    public interface IAuthentication
    {
        Task<string> Post(string username, string password);
    }
}
