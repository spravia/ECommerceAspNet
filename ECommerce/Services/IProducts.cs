using Database.DTO;
using Database.Models;

namespace ECommerce.Services
{
    public interface IProducts
    {

        public IEnumerable<Products> Get();

        public Task<Products?> GetProductById(int id);

        public  Task<ProductsDTO?>? PostAsync(ProductsDTO product);

        public Task<ProductsDTO>? PutAsync(int id, ProductsDTO products);

        public Task<Products>? DeleteAsync(int id);

        public IEnumerable<Products> GetProductsByCategoryId(int id);

    }
}
