using AspNetCoreApplication.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Models
{
    public class Author : BaseModel
    {
        public string Name { get; set; }

        public string Website { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
