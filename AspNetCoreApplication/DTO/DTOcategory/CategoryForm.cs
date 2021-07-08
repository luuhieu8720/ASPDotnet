using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreApplication.DTO.Category
{
    public class CategoryForm
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
