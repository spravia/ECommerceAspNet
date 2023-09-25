using Database.DTO;
using Database.Models;
using ECommerce.Services;
using ECommerce.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text;


namespace ECommerce.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProducts? _products;


        public ProductsController(IProducts products)
        {
            _products = products;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_products?.Get());
        }

        [HttpGet("getProductsByCategoryId")]
        public IActionResult GetProductsByCategoryId(int id)
        {

            return Ok(_products.GetProductsByCategoryId(id));

        }

        [HttpPost("addproduct")]
        public async Task<ActionResult> Post([FromBody] ProductsDTO product)
        {

            var producto = await _products.PostAsync(product);

            if (producto == null)
            {
                
                return BadRequest(new Message("ERR01","Error de los datos "));
            }
            else
            {
                return Ok(product);
            }

        }

        [HttpPut("updateproduct")]
        public async Task<ActionResult> Put(int id,[FromBody] ProductsDTO product)
        {
            var producto = await _products.PutAsync(id, product);

            if (producto == null)
            {
                return BadRequest(new Message("ERR01", "Error al actualizar información "));
            }
            else
            {
                return Ok(product);
            }

        }

        [HttpDelete("deleteproduct")]
        public async Task<ActionResult> Delete(int id)
        {

            var product = await _products.DeleteAsync(id);

            if (product != null)
            {
                return Ok(new Message("SECCESFUL", "Eliminación exitosa"));
            }
            else
            {
                return BadRequest(new Message("ERR01", "Error al eliminar producto"));
            }

        }

    }

}

