using LibraHub.Models;

namespace LibraHub.Repositories.Interface
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetAllBooksWithUserAndCategory();
        Task<Book> GetBookWithUserAndCategory(int id);
        Task<int> GetTotalBookCount();
        Task<int> GetTotalAvailableBookCount();
    }
}
