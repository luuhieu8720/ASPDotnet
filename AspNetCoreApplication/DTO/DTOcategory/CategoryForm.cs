using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.DTOCategory
{
    public class CategoryForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên thể loại không được trống")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mô tả không được trống")]
        public string Description { get; set; }
    }
}
