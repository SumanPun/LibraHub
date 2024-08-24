using LibraHub.Data;
using LibraHub.Models;
using LibraHub.Repositories.Interface;

namespace LibraHub.Repositories
{
    public class BookIssueHistoryRepository : Repository<BookIssueHistory>, IBookIssueHistoryRepository
    {
        public BookIssueHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
