using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOAuthor
{
    public class AuthorDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public DateTime Birthday { get; set; }
        public string Cover { get; set; }
    }
}
