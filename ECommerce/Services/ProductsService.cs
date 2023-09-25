using Database.DTO;
using Database.Models;
using ECommerce.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ECommerce.Services
{
    public class ProductsService : IProducts
    {


        private readonly ECommerceContext? _ECommerceContext;
        private readonly ICategories? _categories;
        private readonly AppSettings _appSettings;

        public ProductsService(ECommerceContext eCommerceContext,
                               IOptions<AppSettings> appSet, 
                               ICategories categories)
        {
            _ECommerceContext = eCommerceContext;
            _appSettings = appSet.Value;
            _categories = categories;
        }


        public async Task<Products>? DeleteAsync(int id)
        {
            
            var producto = await GetProductById(id);

            if (producto != null)
            {
                _ECommerceContext.Remove(producto);
                await _ECommerceContext.SaveChangesAsync();
                return producto;
            }
            else return null;

        }

        public IEnumerable<Products> Get()
        {
            return _ECommerceContext.Products;
        }

        public async Task<Products?> GetProductById(int id)
        {
            try
            {
                return await _ECommerceContext.Products.FirstOrDefaultAsync(product => product.Id == id);
            }catch (Exception ex) 
            {
                return null;
            }

        }

        public IEnumerable<Products>? GetProductsByCategoryId(int id)
        {

           var productos = _ECommerceContext.Products.Where(product => product.CategoriesId == id).ToList();

            if (productos != null) return productos;
            else return null;
         
        }

        public async Task<ProductsDTO?>? PostAsync(ProductsDTO product)
        {

            var productData = new Products();

            //nombre debe ser dif de nulo
            if (product.Name != null)
            {
                product.Name = product.Name.Trim().ToUpper();

                //el producto ya existe
                if ((await _ECommerceContext.Products.FirstOrDefaultAsync(tab => tab.Name == product.Name)) != null)  return null;

                if ((await _categories.GetCategoryById(product.CategoriesId) == null )) return null;
                
                productData.Name = product.Name;
                productData.Price = product.Price;
                productData.Description = product.Description;
                productData.Image1 = product.Image1;
                productData.Image2 = product.Image2;
                productData.CategoriesId = product.CategoriesId;
                productData.Created_at = DateTime.Now;
                productData.Updated_at = DateTime.Now;

                await _ECommerceContext.Products.AddAsync(productData);
                await _ECommerceContext.SaveChangesAsync();

                return product;

            }
            
            return null;

        }

        public async Task<ProductsDTO>? PutAsync(int id, ProductsDTO products)
        {

            var producto = await GetProductById(id);

            if (producto == null) return null;

            if(producto.CategoriesId != products.CategoriesId)
            {
                if (await _categories.GetCategoryById(products.CategoriesId) == null) return null;
                else producto.CategoriesId = products.CategoriesId;
            }

            producto.Description = products.Description;
            producto.Image1 = products.Image1;
            producto.Image2 = products.Image2;
            producto.Price = products.Price;
            producto.Updated_at = DateTime.Now;

            _ECommerceContext.Entry<Products>(producto).CurrentValues.SetValues(producto);
            await _ECommerceContext.SaveChangesAsync();
            
            return products;            

        }
    }
}
