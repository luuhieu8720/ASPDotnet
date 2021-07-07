using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Models
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
