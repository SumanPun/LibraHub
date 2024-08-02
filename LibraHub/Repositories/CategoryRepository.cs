using LibraHub.Data;
using LibraHub.Models;
using LibraHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraHub.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAllCategoriesWithUser()
        {
            return await _context.Categories!.Include(c => c.CreatedUser).Include(m => m.ModifiedUser).ToListAsync();
        }

        public async Task<Category> GetCategoryWithUser(int id)
        {
            return await _context.Categories!.Include(c => c.CreatedUser).Include(m => m.CreatedUser).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Category not found");
        }
    }
}
