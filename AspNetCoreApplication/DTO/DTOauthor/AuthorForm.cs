using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOAuthor
{
    public class AuthorForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing website")]
        public string Website { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing birthday")]
        public DateTime? Birthday { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Missing cover")]
        public string Cover { get; set; }
    }
}
