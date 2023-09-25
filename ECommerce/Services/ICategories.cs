using Database.DTO;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services
{
    public interface ICategories
    {

        public IEnumerable<Categories> Get();

        public Task<Categories?> GetCategoryById(int id);


        public Task<Categories>? Post(CategoriesDTO categorie);
                
        public Task<CategoriesDTO>? Put(int id, CategoriesDTO categories, string errorDesc);

        public Task<Categories?>? Delete(int id);

    }
}
