using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.DTO
{
    
    public class ProductsDTO
    {
        public string? Name { get; set; } = null;

        public string? Description { get; set; } = null;

        public decimal? Price { get; set; } = null;

        public int CategoriesId { get; set; }

        public string? Image1 { get; set; } = null;

        public string? Image2 { get; set; } = null;

    }
}
