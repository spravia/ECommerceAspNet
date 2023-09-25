using Database.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class UsuarioEcommerceController : ControllerBase
    {

        private readonly IUsuarioECommerce _usuarioEcommerce;
        

        public UsuarioEcommerceController(IUsuarioECommerce usuarioEcommerce)
        {
            _usuarioEcommerce = usuarioEcommerce;
        }


        [HttpGet()]
        public IActionResult Get()
        {
            return Ok(_usuarioEcommerce.Get());

            /*
            return new JsonResult("No encontrado")
            {
                StatusCode = StatusCodes.Status200OK
            };
             */

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var usuario = await _usuarioEcommerce.GetAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(await _usuarioEcommerce.GetAsync(id));

        }


        [HttpPost]
        public async Task<ActionResult> Auth([FromBody] Auth auth)
        {

            try { 
                var response = await _usuarioEcommerce.LoginAsync(auth.email, auth.password);

          
                if (response == null)
                {
                  //return new JsonResult( new HttpRequestException("Error encontrado Savas", null, HttpStatusCode.NotFound));
                  return NotFound("Email no encontrado");
                }

                return Ok(response);

            }catch(Exception e)
             {
                return BadRequest(e.Message);
             }

        }


        [HttpPost("adduser")]
        public async Task Post([FromBody] UsuarioECommerceTable user)
        {

            if ((user.Email.Trim() == " ") || (await _usuarioEcommerce.PostAsync(user) == null))
            {
                BadRequest();
                return;
            }

            Ok();  //Ok 
            return;
        }
       
    }
}
