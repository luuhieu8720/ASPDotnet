using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApplication.Models;

namespace AspNetCoreApplication.DTO.DTOCategory
{
    public class CategoryDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // public List<BookDetail> Books { get; set; }
    }
}
