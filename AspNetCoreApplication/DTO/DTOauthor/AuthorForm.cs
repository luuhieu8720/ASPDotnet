using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOAuthor
{
    public class AuthorForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên tác giả không được để trống")]
        public string Name { get; set; }

        public string Website { get; set; }

        public string Cover { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
