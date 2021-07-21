using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOuser
{
    public class CurrentUser : ClaimsPrincipal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
