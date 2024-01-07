using MoviesWebApp.Models;

namespace ProductsWebApp.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetAsync(int id);

        Task<Product> AddAsync(Product product);

        Task<Product?> UpdateAsync(Product product);

        Task<Product?> DeleteAsync(int id);
    }
}
