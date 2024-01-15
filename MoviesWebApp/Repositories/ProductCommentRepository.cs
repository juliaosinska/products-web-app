using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using ProductsWebApp.Models.Domain;

namespace ProductsWebApp.Repositories
{
	public class ProductCommentRepository : IProductCommentRepository
	{
		private readonly ProductsDbContext productsDbContext;

		public ProductCommentRepository(ProductsDbContext productsDbContext)
        {
			this.productsDbContext = productsDbContext;
		}

        public async Task<ProductComment> AddAsync(ProductComment productComment)
		{
			await productsDbContext.ProductComments.AddAsync(productComment);
			await productsDbContext.SaveChangesAsync();

			return productComment;
		}

		public async Task<IEnumerable<ProductComment>> GetCommentsByProductIdAsync(int productId)
		{
			return await productsDbContext.ProductComments.Where(x => x.ProductId == productId).ToListAsync();
		}
	}
}
