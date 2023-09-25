using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services
{
    public interface IUsuarioECommerce
    {

        public IEnumerable<UsuarioECommerceTable> Get();

        public Task<UsuarioECommerceTable> GetUserById(int id);

        public Task<RegisterResponse> PostAsync(UserDataNew usuarioEcommerce);

        public Task<UsuarioECommerceTable> PutAsync(int id, UserData userdata);
        
        public Task<ActionResult<UsuarioECommerceTable>> DeleteAsync (int id);

        public Task<UserData> LoginAsync (string username, string password);

        //public AuthResponse Autenticacion(AuthRequest data);

    }
}
