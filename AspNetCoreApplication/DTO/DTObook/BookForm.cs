using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTObook
{
    public class BookForm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<BookCategory> Categories { get; set; }
    }
}
