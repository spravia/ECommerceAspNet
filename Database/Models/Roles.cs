using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Database.Models
{
    [Table("Roles")]
    public class Roles
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NombreRole { get; set; }

        public string Image { get; set; }

        public string Route{ get; set;}

        public int IdUsuarioECommerce { get; set; }
        public UsuarioECommerceTable? UsuarioECommerceTable { get; set; }

        
    }
}


