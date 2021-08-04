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
using Microsoft.AspNetCore.Authorization;
using AspNetCoreApplication.DTO.DTOAuths;

namespace AspNetCoreApplication.Controller
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
        public async Task<TokenResponse> Post([FromBody] LoginForm loginForm) => await authenticationService.Login(loginForm);

        [HttpGet]
        [Authorize]
        public AuthenUser Get() => authenticationService.CurrentUser;
    }
}
