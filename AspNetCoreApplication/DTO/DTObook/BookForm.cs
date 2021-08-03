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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên sách không được để trống")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mô tả sách không được để trống")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Giá bán không được để trống")]
        public double Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Năm phát hành không được để trống")]
        public int Year { get; set; }

        public int AuthorId { get; set; }

        public string Cover { get; set; }

        public List<int> CategoryId { get; set; }
    }
}
