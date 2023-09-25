using Database.DTO;
using Database.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ECommerce.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategories? _categories;


        public CategoriesController(ICategories categories)
        {
            _categories = categories;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categories.Get());
        }

        [HttpPost("addcategorie")]
        public async Task<ActionResult> Post([FromBody] CategoriesDTO categories)
        {

            var categori = await _categories.Post(categories);

            if ( categori == null )
            {
                return BadRequest();
            }
            else
            {
                return Ok(categori);
            }

             
        }

        [HttpPut("updatecategorie")]
        public async Task<ActionResult> Put( int id,[FromBody] CategoriesDTO categories)
        {
            var cat = await _categories.GetCategoryById(id);
            var errorDesc = "";

            if (cat != null)
            {
                
                if (await _categories.Put(id, categories, errorDesc) != null)
                {
                    return Ok(categories);
                }else { 
                    return BadRequest(errorDesc); 
                }
            }
            else
            {
               return BadRequest("Categoria no encontrada");
            }

        }


        [HttpDelete("deletecatetorie")]
        public async Task<ActionResult> Delete(int id)
        {

             var categorie = await _categories?.Delete(id);

            if (categorie != null )
            {
                return Ok(categorie);
            }
            else
            {
                return BadRequest("Error: not found");
            }
        }

    }
}
