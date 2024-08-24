using LibraHub.Models;

namespace LibraHub.Repositories.Interface
{
    public interface IBookIssueRepsoitory : IRepository<BookIssue>
    {
        Task<List<BookIssue>> GetAllBookIssuesWithProperties();
        Task<List<BookIssue>> GetAllBookIssuesWithPropertiesNotReturned();
        Task<List<BookIssue>> GetAllBookIssuesWithPropertiesReturned();
        Task<BookIssue> GetBookIssueWithProperties(int id);
        Task<int> GetTotalBookIssueCount();
        Task<int> GetTotalBookReturnCount();
    }
}
