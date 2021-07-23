using AspNetCoreApplication.Services;
using AspNetCoreApplication.DTO.DTOUser;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApplication.DTO.DTOuser;

namespace AspNetCoreApplication.Authentications
{
    [Route("api/auths")]
    public class AuthenticatesController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticatesController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<TokenResponse> Post(string username, string password) => await authenticationService.Login(username, password);

        [HttpGet]
        public UserClaimsPrincipal Get() => authenticationService.GetCurrentUser();
    }
}
