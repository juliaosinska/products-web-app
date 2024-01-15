
using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using ProductsWebApp.Models.Domain;

namespace ProductsWebApp.Repositories
{
    public class ProductLikeRepository : IProductLikeRepository
    {
        private readonly ProductsDbContext productsDbContext;

        public ProductLikeRepository(ProductsDbContext productsDbContext)
        {
            this.productsDbContext = productsDbContext;
        }

		public async Task<ProductLike> AddLikeForProduct(ProductLike productLike)
		{
            await productsDbContext.ProductLikes.AddAsync(productLike);
            await productsDbContext.SaveChangesAsync();

            return productLike;
		}

		public async Task<IEnumerable<ProductLike>> GetLikesForProduct(int productId)
		{
			return await productsDbContext.ProductLikes.Where(x => x.ProductId == productId).ToListAsync();
		}

		public async Task<int> GetTotalLikes(int productId)
        {
            return await productsDbContext.ProductLikes.CountAsync(x => x.ProductId == productId);
        }
    }
}
