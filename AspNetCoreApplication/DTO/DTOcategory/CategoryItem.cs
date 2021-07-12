using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApplication.DTO.DTOcategory
{
    public class CategoryItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
