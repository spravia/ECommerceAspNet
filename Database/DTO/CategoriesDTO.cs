using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO
{
    public class CategoriesDTO
    {
        public string? name { get; set; }

        public string? description { get; set; } = null;

        public string? image { get; set; } = null;
    }
}
