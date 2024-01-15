using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Models;
using ProductsWebApp.Models.Domain;

namespace MoviesWebApp.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<ProductLike> ProductLikes { get; set; }

		public DbSet<ProductComment> ProductComments { get; set; }
	}
}
