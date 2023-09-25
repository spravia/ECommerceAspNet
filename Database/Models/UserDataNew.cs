using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class UserDataNew
    {

        public int id  { get; set; }

        public string nombres { get; set; }

        public string apellidos { get; set; }   
       
        public string email { get; set; }

        public string password { get; set; }

        public int? telefono { get; set; }    

        public Roles? roles { get; set; }
    }
}
