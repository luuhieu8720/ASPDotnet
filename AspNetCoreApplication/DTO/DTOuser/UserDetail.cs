﻿using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOuser
{
    public class UserDetail
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime LastOnline { get; set; }

        public Role Role { get; set; }
    }
}
