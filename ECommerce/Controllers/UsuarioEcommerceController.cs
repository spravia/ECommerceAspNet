using Azure;
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
            var usuario = await _usuarioEcommerce.GetUserById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(await _usuarioEcommerce.GetUserById(id));

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
        public async Task<ActionResult> Post([FromBody] UserDataNew userNew)
        {
            if (userNew.email.Trim() != " ")
            {
                var response = await _usuarioEcommerce.PostAsync(userNew);

                if(response.errorcode == 0)
                {
                    return Ok(response);  //Ok 
                    
                }
                else
                {
                    return BadRequest(response);
                }               
            }else return BadRequest();
        }

        [HttpPut("updateuser")]
        public async Task<ActionResult> Put(int id, [FromBody] UserData userdata)
        {
            var usu = await _usuarioEcommerce.GetUserById(id);

            if (usu == null)
            {
                return BadRequest();

            }
            else
            {
                
                if (await _usuarioEcommerce.PutAsync(id, userdata) != null)
                {
                    return Ok(userdata);
                }
                else
                {
                    return BadRequest(); 
                }
                                    
            }

        }

    }
}
