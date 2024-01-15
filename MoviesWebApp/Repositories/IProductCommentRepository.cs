using ProductsWebApp.Models.Domain;

namespace ProductsWebApp.Repositories
{
	public interface IProductCommentRepository
	{
		Task<ProductComment> AddAsync(ProductComment productComment);

		Task<IEnumerable<ProductComment>> GetCommentsByProductIdAsync(int productId);
	}
}
