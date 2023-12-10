using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Models;

namespace MoviesWebApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MoviesDbContext moviesDbContext;

        public CategoryRepository(MoviesDbContext moviesDbContext)
        {
            this.moviesDbContext = moviesDbContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await moviesDbContext.Category.AddAsync(category);
            await moviesDbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var existingCategory = await moviesDbContext.Category.FindAsync(id);

            if (existingCategory != null)
            {
                moviesDbContext.Category.Remove(existingCategory);
                await moviesDbContext.SaveChangesAsync();

                return existingCategory;
            }

            return null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await moviesDbContext.Category.ToListAsync();
        }

        public Task<Category?> GetAsync(int id)
        {
            return moviesDbContext.Category.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await moviesDbContext.Category.FindAsync(category.id);

            if (existingCategory != null) { }
            {
                existingCategory.name = category.name;

                await moviesDbContext.SaveChangesAsync();

                return existingCategory;
            }
            return null;


        }
    }
}
