using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace ProductsWebApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext productsDbContext;

        public ProductRepository(ProductsDbContext productsDbContext)
        {
            this.productsDbContext = productsDbContext;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await productsDbContext.AddAsync(product);
            await productsDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var existingProduct = await productsDbContext.Product.FindAsync(id);

            if (existingProduct != null)
            {
                existingProduct.IsDeleted = 1;
                await productsDbContext.SaveChangesAsync();
                return existingProduct;
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await productsDbContext.Product.Include(x=> x.Categories).Where(x => x.IsDeleted == 0).ToListAsync();
        }

        public async Task<Product?> GetAsync(int id)
        {
            return await productsDbContext.Product.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var existingProduct =  await productsDbContext.Product.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Title = product.Title;
                existingProduct.Description = product.Description;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.Categories = product.Categories;

                await productsDbContext.SaveChangesAsync();
                return existingProduct;
            }
            return null;
        }
    }
}
