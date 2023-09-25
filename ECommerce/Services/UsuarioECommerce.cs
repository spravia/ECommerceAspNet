using Database.Models;
using ECommerce.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Services
{
    public class UsuarioECommerce : IUsuarioECommerce
    {

        private readonly ECommerceContext _usuarioECommerceContext;
        private readonly AppSettings _appSettings;
               
        private readonly RegisterResponse _registerResponse;

        //Constructor de la clase
        public UsuarioECommerce(ECommerceContext usuarioECommerceContext,
            IOptions<AppSettings> appSet)
        {
            _usuarioECommerceContext = usuarioECommerceContext;
            _appSettings = appSet.Value;
        }
                

        public async Task<ActionResult<UsuarioECommerceTable>> DeleteAsync(int id)
        {
            var usu = await _usuarioECommerceContext.UsuECom.FirstOrDefaultAsync(usu => usu.Id == id);

            if(usu != null)
            {
                _usuarioECommerceContext.Remove(usu);
                await _usuarioECommerceContext.SaveChangesAsync();
            }

            return usu;
        }

        public IEnumerable<UsuarioECommerceTable> Get()
        {
            return _usuarioECommerceContext.UsuECom;
        }

        public async Task<UsuarioECommerceTable?> GetUserById(int id)
        {
            
            var usu = await _usuarioECommerceContext.UsuECom.FirstOrDefaultAsync(usu => usu.Id == id);
            if(usu != null)
            {
                return usu;
            }else
            {
                return null;
            }        
           }

        public async Task<UserData> LoginAsync(string email, string password)
        {
            var userData = new UserData();
            
            var user = await _usuarioECommerceContext.UsuECom.FirstOrDefaultAsync(mail => mail.Email == email);

            if (user != null)
            {

                var passSaved = user.Password;

                if(passwordIsValid(email, password, passSaved))
                { 
                  userData.id = user.Id;
                  userData.nombres = user?.Nombres;
                  userData.apellidos = user?.Apellidos;
                  userData.telefono = user?.Telefono.ToString();
                    userData.correo = email.Trim();
                  userData.roles = "CLIENT"; 

                  return userData;

                } 
            }

            return null;

        }

        public Boolean passwordIsValid(string email, string password, string passSaved)
        {

            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;

            //** Transfer Data Object  DTO  https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
            //** https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/compute-hash-values
            //** https://code-maze.com/csharp-hashing-salting-passwords-best-practices/


            sSourceData = password.Trim();
                
            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            password = BitConverter.ToString(tmpHash);

            if (password == passSaved)
            {
                return true;
            }else 
                return false;


            /*
            //  Begin *** encrypt password
            sSourceData = password;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);


            string cadena = "bconic505.";
            tmpSource = ASCIIEncoding.ASCII.GetBytes(cadena);

            byte[] tmpHash2;
            tmpHash2  = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            bool bEqual = false;
            if (tmpHash2.Length  == tmpHash.Length)
            {
                int i = 0;
                while ((i < tmpHash2.Length) && (tmpHash2[i] == tmpHash[i]))
                {
                    i += 1;
                }
                if (i == tmpHash2.Length)
                {
                    bEqual = true;
                }
            }

            //  End *** encrypt password
            password = BitConverter.ToString(tmpHash);

            // Valida el correo
            if (_usuarioECommerceContext.UsuECom.FirstOrDefault(mail => mail.Email == email) != null)
            {

            }

                */
            //await _usuarioECommerceContext.UsuECom.AddAsync(usuarioEcommerce);
            //await _usuarioECommerceContext.SaveChangesAsync();


        }

        public async Task<RegisterResponse?> PostAsync(UserDataNew? usuario)
        {

            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;
            string password;

            var UsuarioECommerce = new UsuarioECommerceTable();
            var RolesUsu = new Roles();
            var _registerResponse = new RegisterResponse();
            
            usuario.email = usuario.email.Trim();
            UsuarioECommerceTable? user = await _usuarioECommerceContext.UsuECom.FirstOrDefaultAsync(mail => mail.Email == usuario.email);

            if (user == null)
            {
                _registerResponse.errorcode = 0;
                _registerResponse.errorDesc = "Success";

                sSourceData = usuario.password;
                tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
                tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                password = BitConverter.ToString(tmpHash);
                usuario.password = password;

                UsuarioECommerce.Nombres = usuario.nombres;
                UsuarioECommerce.Apellidos = usuario.apellidos;
                UsuarioECommerce.Email = usuario.email;
                UsuarioECommerce.Password = usuario.password;
                UsuarioECommerce.Telefono = usuario.telefono;


                UsuarioECommerce.Roles.NombreRole = usuario.roles.NombreRole;
                UsuarioECommerce.Roles.Image = "";
                UsuarioECommerce.Roles.Route = "";
                UsuarioECommerce.Roles.IdUsuarioECommerce = usuario.id;
                
                await _usuarioECommerceContext.UsuECom.AddAsync(UsuarioECommerce);
                await _usuarioECommerceContext.SaveChangesAsync();
                return _registerResponse;
            }
            else
            {

                _registerResponse.errorcode = 1;
                _registerResponse.errorDesc = "Error de datos";
                return _registerResponse;
            }

        }

        public async Task<UsuarioECommerceTable?> PutAsync(int id, UserData userdata)
        {
            var usuarioEcommerce = await _usuarioECommerceContext.UsuECom.FirstOrDefaultAsync(usu => usu.Id == id);

            if(usuarioEcommerce != null)
            {

                usuarioEcommerce.Telefono = Int32.Parse(userdata.telefono);
                usuarioEcommerce.Nombres = userdata.nombres;
                usuarioEcommerce.Apellidos = userdata.apellidos;

                _usuarioECommerceContext.Entry<UsuarioECommerceTable>(usuarioEcommerce).CurrentValues.SetValues(usuarioEcommerce);
                await _usuarioECommerceContext.SaveChangesAsync();
            }

            return usuarioEcommerce;
        }

       
    }
}
