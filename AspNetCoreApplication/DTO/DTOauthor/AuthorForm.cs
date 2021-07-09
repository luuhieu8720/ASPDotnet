using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOauthor
{
    public class AuthorForm
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public DateTime Birthday { get; set; }
        public string Cover { get; set; }
    }
}
