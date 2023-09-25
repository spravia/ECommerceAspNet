using Database.DTO;
using Database.Models;
using ECommerce.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;

namespace ECommerce.Services
{
    public class CategoriesService : ICategories
    {

        private readonly ECommerceContext _eCommerceContext;
        private readonly AppSettings _appSettings;


        public CategoriesService(ECommerceContext eCommerceContext,
            IOptions<AppSettings> appSet)
        {
            _eCommerceContext = eCommerceContext;
            _appSettings = appSet.Value;
        }

        public IEnumerable<Categories> Get()
        {
            return _eCommerceContext.Categories;
        }

        public async Task<Categories?> GetCategoryById(int id)
        {

            return await _eCommerceContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        }
            
        public async Task<Categories>? Post(CategoriesDTO categorie)
        {

            var categorieData = new Categories();

            if (categorie.name != null)
            {
                categorie.name = categorie?.name?.Trim().ToUpper();
                if ((await _eCommerceContext.Categories.FirstOrDefaultAsync(name => name.name == categorie.name) != null))
                {
                    return null;
                }
            }else return null;
            
            categorieData.name = categorie?.name;
            categorieData.description = categorie?.description;
            categorieData.image = categorie?.image;
            categorieData.updated_at = DateTime.Now;

            await _eCommerceContext.Categories.AddAsync(categorieData);
            await _eCommerceContext.SaveChangesAsync();
            return categorieData;
        }
        
        public async Task<CategoriesDTO?>? Put(int id, CategoriesDTO categories, string errorDesc)
        {

            categories.name = categories?.name?.Trim().ToUpper();

            var categoriesName = await _eCommerceContext.Categories.FirstOrDefaultAsync(c => c.name == categories.name);

            if (categoriesName == null)
            {
                errorDesc = "Categoria no encontrada";
                return null;
            }
            
            var cateById = await _eCommerceContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (cateById != null)
            {
                cateById.description = categories?.description;
                cateById.updated_at = DateTime.Now;
                _eCommerceContext.Entry<Categories>(cateById).CurrentValues.SetValues(categories);
                await _eCommerceContext.SaveChangesAsync();
                return categories;
            }
            else
            {
                errorDesc = "Categoria no encontrada";
                return null;
            }

        }

        public async Task<Categories?>? Delete(int id)
        {

            var cateById = await _eCommerceContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (cateById != null)
            {
                _eCommerceContext.Remove(cateById);
                await _eCommerceContext.SaveChangesAsync();
                return cateById;
            }
            else
            { 
                return null;
            }
        }
     }
}
