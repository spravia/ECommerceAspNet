using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string? Description { get; set; }

        public string? Image1 { get; set; } = null;

        public string? Image2 { get; set; } = null;

        public decimal? Price { get; set; } = null;

        [DataType(DataType.DateTime)]
        public System.DateTime Created_at { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public System.DateTime? Updated_at { get; set; }

        public int CategoriesId { get; set; }

    }
}
