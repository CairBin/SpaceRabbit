using CategoryService.Domain;
using CategoryService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Infrastructure
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly CategoryDbContext _dbCtx;
        public CategoryRepo(CategoryDbContext dbCtx)
        {
            this._dbCtx = dbCtx;
            
        }

        public async Task<Guid> CreateAsync(Category category)
        {
            await this._dbCtx.AddAsync(category);
            return category.Id;
        }

        public async Task<Category?> FindByIdAsync(Guid id)
        {
            var category = await this._dbCtx.Categories.FindAsync(id);
            return category;
        }

        public async Task<Category?> FindByNameAsync(string name)
        {
            var category = await this._dbCtx.Categories.SingleOrDefaultAsync(x => x.Name == name);
            return category;
        }

        public async Task<Category[]> GetAllAsync()
        {
            return await this._dbCtx.Categories.OrderByDescending(x => x.Created).ToArrayAsync();
        }

    }
}
