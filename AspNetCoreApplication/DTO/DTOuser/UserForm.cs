using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOUser
{
    public class UserForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing username")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing password")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing birthday")]
        public DateTime? Birthday { get; set; }

        public DateTime LastOnline { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing role")]
        public Role? Role { get; set; }
    }
}
