using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
        public List<BookCategory> Categories { get; set; }

    }
}
