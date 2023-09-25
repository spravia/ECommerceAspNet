using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    [Table("UsuarioECommerceTable")]
    public class UsuarioECommerceTable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombres { get; set; }

        public string? Apellidos { get; set; }
        public string Email { get; set; }

        public int? Telefono { get; set; }

        public string Password { get; set; }

        public Roles? Roles { get; set; }


        public UsuarioECommerceTable() { 
               Roles = new Roles();
        }
    }
}
