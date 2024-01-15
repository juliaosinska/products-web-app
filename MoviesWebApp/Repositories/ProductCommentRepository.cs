using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Models;
using ProductsWebApp.Models.Domain;
using System.Runtime.InteropServices;

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
			return await productsDbContext.ProductComments.Where(x => x.ProductId == productId && x.IsDeleted == 0).ToListAsync();
		}

        public async Task<ProductComment> DeleteAsync(int id)
        {
			var existingComment = await productsDbContext.ProductComments.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == 0);

            if (existingComment != null)
            {
				existingComment.IsDeleted = 1;
                await productsDbContext.SaveChangesAsync();

                return existingComment;
            }

            return null;
        }

		public async Task<ProductComment> GetAsync(int id)
		{
			return await productsDbContext.ProductComments.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == 0);
		}
	}
}
