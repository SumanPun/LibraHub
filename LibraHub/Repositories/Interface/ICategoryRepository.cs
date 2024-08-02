using LibraHub.Models;

namespace LibraHub.Repositories.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetAllCategoriesWithUser();
        Task<Category> GetCategoryWithUser(int id);
    }
}
