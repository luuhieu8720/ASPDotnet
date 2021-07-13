using AspNetCoreApplication.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Models
{
    public class Category : BaseModel
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BookCategory> Books { get; set; }
    }
}
