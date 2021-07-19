using AspNetCoreApplication.Config;
using AspNetCoreApplication.DTO.DTOUser;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Authentications
{
    [Route("api/auths")]
    public class AuthenticatesController : ControllerBase
    {
        private readonly IAuthentication authentication;

        public AuthenticatesController(IAuthentication authentication)
        {
            this.authentication = authentication;
        }

        [HttpPost]
        public async Task<string> Post(string username, string password) => await authentication.Post(username, password);

    }
}
