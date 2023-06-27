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

        public async Task<UsuarioECommerceTable?> GetAsync(int id)
        {
            return await _usuarioECommerceContext.UsuECom.FirstOrDefaultAsync(usu => usu.Id == id);
        }

        public async Task<UserData?> LoginAsync(string email, string password)
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
                  userData.roles = ""; 

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

        public async Task<UsuarioECommerceTable?> PostAsync(UsuarioECommerceTable? usuarioEcommerce)
        {

            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;
            string password;

            usuarioEcommerce.Email = usuarioEcommerce.Email.Trim();
            UsuarioECommerceTable? user = await _usuarioECommerceContext.UsuECom.FirstOrDefaultAsync(mail => mail.Email == usuarioEcommerce.Email);

            if (user == null)
            {

                sSourceData = usuarioEcommerce.Password;
                tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
                tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                password = BitConverter.ToString(tmpHash);
                usuarioEcommerce.Password = password;

                await _usuarioECommerceContext.UsuECom.AddAsync(usuarioEcommerce);
                await _usuarioECommerceContext.SaveChangesAsync();
                return usuarioEcommerce;
            }
            else
            {
                return null;
            }

        }

        public async Task<UsuarioECommerceTable> PutAsync(int id, UsuarioECommerceTable usuarioEcommerce)
        {
            var usu = await _usuarioECommerceContext.UsuECom.FirstOrDefaultAsync(usu => usu.Id == id);

            if(usu != null)
            {
                _usuarioECommerceContext.Entry<UsuarioECommerceTable>(usu).CurrentValues.SetValues(usuarioEcommerce);
                await _usuarioECommerceContext.SaveChangesAsync();

            }

            return usuarioEcommerce;
        }

       
    }
}
