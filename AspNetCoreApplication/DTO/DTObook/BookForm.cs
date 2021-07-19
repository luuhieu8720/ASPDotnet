using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOBook
{
    public class BookForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing description")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing price")]
        public double Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing published year")]
        public int Year { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing author ID")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing cover")]
        public string Cover { get; set; }

        public List<BookCategory> Categories { get; set; }
    }
}
