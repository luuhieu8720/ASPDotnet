using AspNetCoreApplication.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime LastOnline { get; set; }
       
        public Role Role{ get; set; }
    }
}
