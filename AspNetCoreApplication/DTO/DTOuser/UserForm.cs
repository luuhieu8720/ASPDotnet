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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên người dùng không được để trống")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên tài khoảng không được để trống")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime LastOnline { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing role")]
        public Role? Role { get; set; }
    }
}