using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Models;

namespace MoviesWebApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductsDbContext productsDbContext;

        public CategoryRepository(ProductsDbContext productsDbContext)
        {
            this.productsDbContext = productsDbContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await productsDbContext.Category.AddAsync(category);
            await productsDbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var existingCategory = await productsDbContext.Category.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == 0);

            if (existingCategory != null)
            {
                existingCategory.IsDeleted = 1; 
                await productsDbContext.SaveChangesAsync();

                return existingCategory;
            }

            return null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await productsDbContext.Category.Where(x => x.IsDeleted == 0).ToListAsync();
        }

        public Task<Category?> GetAsync(int id)
        {
            return productsDbContext.Category.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == 0);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await productsDbContext.Category.FirstOrDefaultAsync(x => x.Id == category.Id && x.IsDeleted == 0);

            if (existingCategory != null) { }
            {
                existingCategory.Name = category.Name;

                await productsDbContext.SaveChangesAsync();

                return existingCategory;
            }
            return null;


        }
    }
}
