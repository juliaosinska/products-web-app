using ProductsWebApp.Models.Domain;

namespace ProductsWebApp.Repositories
{
    public interface IProductLikeRepository
    {
        Task<int> GetTotalLikes(int productId);

        Task<ProductLike> AddLikeForProduct(ProductLike productLike);

        Task<IEnumerable<ProductLike>> GetLikesForProduct(int productId);
    }
}
