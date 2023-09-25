using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Models
{
    public class Categories
    {
        private string? created_at1 = Convert.ToString((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? name  { get; set; }    

        public string? description { get; set; } = null;

        public string? image{ get; set; } = null;

        [DataType(DataType.DateTime)]
        public System.DateTime created_at { get ; set ; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public System.DateTime? updated_at { get; set; }

        public  List<Products> Products { get; set; }

        
        public Categories() 
        { 
             List<Products> products = new List<Products>();  
        }
            }
}
