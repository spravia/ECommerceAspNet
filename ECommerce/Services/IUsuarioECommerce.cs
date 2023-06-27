using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services
{
    public interface IUsuarioECommerce
    {

        public IEnumerable<UsuarioECommerceTable> Get();

        public Task<UsuarioECommerceTable> GetAsync(int id);

        public Task<UsuarioECommerceTable> PostAsync(UsuarioECommerceTable usuarioEcommerce);

        public Task<UsuarioECommerceTable> PutAsync(int id, UsuarioECommerceTable usuarioEcommerce);
        
        public Task<ActionResult<UsuarioECommerceTable>> DeleteAsync (int id);

        public Task<UserData> LoginAsync (string username, string password);


        //public AuthResponse Autenticacion(AuthRequest data);

    }
}
