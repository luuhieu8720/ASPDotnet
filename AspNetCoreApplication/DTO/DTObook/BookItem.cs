using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApplication.DTO.DTObook
{
    public class BookItem
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
